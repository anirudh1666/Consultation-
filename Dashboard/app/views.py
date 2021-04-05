from django.shortcuts import render
from .models import Query, User, TestQuery, TestUser, ClickedUrl, Recommendation
from .refresh import compute_all_data, load_query_data_with_parameters
from django.http import JsonResponse
from django.views.decorators.csrf import csrf_exempt
import datetime


# Change this when deploying. Right now we are using QUERY_TABLE and USER_TABLE to display some
# dummy data in dashboard. In the actual app we will use the User and Query table so all you have to 
# do is change these variables.

USER_TABLE = TestUser         # Change to User when deploying.
QUERY_TABLE = TestQuery       # Change to Query when deploying.

# Create your views here.
def index(request):
    return render(request, 'app/index.html')

'''Auxiliary function to calculate the start of the week for the past 5 weeks.
   @param today is datetime.datetime.now().
   @returns array containinng datetime.datetime objects starting from 5 weeks ago to today.'''
def calculateDates(today, num_weeks=5):

    return_array = [today]

    for _ in range(num_weeks):
        return_array.append(return_array[-1] - datetime.timedelta(days = 7))

    return return_array[::-1] #Reverses to return desired format

'''Gets all the requests in a specified location. Works by first fetching all queries
   within the time frame and then checking if the user who sent that query was in the specified location.
   @param an ajax GET request sent from dashboard.js. Contains the location we are searching for.
   @returns json response containing an array of {value : no of requests, year : year, month: month, day : day} dicts. 
            This format makes it easy for dashboard.js to process this data. There are 5 dicts for the last 5 weeks.'''
def getRequests(request):

    if request.method == 'GET':
        location = request.GET.get('location')
        today = datetime.datetime.now()
        weeks = calculateDates(today)
        ret = []
        for i in range(len(weeks) - 1):
            count = 0
            queries = QUERY_TABLE.objects.filter(date__range=[weeks[i], weeks[i+1]])
            users = USER_TABLE.objects.filter(location=location)
            for query in queries:
                if query.user not in users:
                    continue
                count += 1
            ret.append({'value' : count, 'year' : weeks[i+1].year, 'month' : weeks[i+1].month, 'day' : weeks[i+1].day})

        return JsonResponse(ret, status=200, safe=False)
    return JsonResponse({}, status=200)

'''Gets all the newly registered users in a specified location and within the past 5 weeks. Works by simply
   filtering out all users that were created out of the time range and not in the location specified by request.
   @request is http get request sent by dashboard.js. the request contains the location we are searching through.
   @returns json response containing an array of {value : no of users, year : year, month: month, day : day} dicts. 
            This format makes it easy for dashboard.js to process this data. There are 5 dicts for the last 5 weeks.'''
def getNewUsers(request):

    if request.method == 'GET':
        location = request.GET.get('location')
        today = datetime.datetime.now()
        weeks = calculateDates(today)
        ret = []
        for i in range(len(weeks) - 1):
            count = len(USER_TABLE.objects.filter(date_registered__range=[weeks[i], weeks[i + 1]], location=location))
            ret.append({'value' : count, 'year' : weeks[i+1].year, 'month' : weeks[i+1].month, 'day' : weeks[i+1].day})

        return JsonResponse(ret, status=200, safe=False)
    return JsonResponse({}, status=200)

'''Gets all the unique requests in a specified location over the past 5 weeks. Works similarly to getRequests but also
   filters out queries that have already been seen. 
   @request is http get request sent by dashboard.js. the request contains the location we are searching through.
   @returns json response containing an array of {value : no of unique requests, year : year, month: month, day : day} dicts. 
            This format makes it easy for dashboard.js to process this data. There are 5 dicts for the last 5 weeks.'''
def getUniqRequests(request):

    if request.method == 'GET':
        location = request.GET.get('location')
        today = datetime.datetime.now()
        weeks = calculateDates(today)
        ret = []
        for i in range(len(weeks) - 1):
            seen = []
            count = 0
            queries = QUERY_TABLE.objects.filter(date__range=[weeks[i], weeks[i+1]])
            users = USER_TABLE.objects.filter(location=location)
            for query in queries:
                if query.user not in users or query.search_term in seen:
                    continue
                seen.append(query.search_term)
                count += 1
            ret.append({'value' : count, 'year' : weeks[i+1].year, 'month' : weeks[i+1].month, 'day' : weeks[i+1].day})

        return JsonResponse(ret, status=200, safe=False)
    return JsonResponse({}, status=200)

'''This is similar to getNewUsers but this doesn't filter based off location. It gets all new users in the past 5 weeks. It 
   assumes that all users are in the UK. 
   @request is http get request sent by dashboard.js. 
   @returns json response containing an array of {value : no of users, year : year, month: month, day : day} dicts. 
            This format makes it easy for dashboard.js to process this data. There are 5 dicts for the last 5 weeks.'''
def getAllNewUsers(request):

    if request.method == 'GET':
        today = datetime.datetime.now()
        weeks = calculateDates(today)
        ret = []
        for i in range(len(weeks) - 1):
            count = len(USER_TABLE.objects.filter(date_registered__range=[weeks[i], weeks[i+1]]))
            ret.append({'value' : count, 'year' : weeks[i+1].year, 'month' : weeks[i+1].month, 'day' : weeks[i+1].day})

        return JsonResponse(ret, status=200, safe=False)
    return JsonResponse({}, status=200)

'''Gets the total requests made per week over the past 5 weeks. Doesn't filter using location.
   @request is http get request sent by dashboard.js. 
   @returns json response containing an array of {value : no of users, year : year, month: month, day : day} dicts. 
            This format makes it easy for dashboard.js to process this data. There are 5 dicts for the last 5 weeks.'''
def getAllRequests(request):
    
    if request.method == 'GET':
        today = datetime.datetime.now()
        weeks = calculateDates(today)
        ret = []
        for i in range(len(weeks) - 1):
            count = len(QUERY_TABLE.objects.filter(date__range=[weeks[i], weeks[i+1]]))
            ret.append({'value' : count, 'year' : weeks[i+1].year, 'month' : weeks[i+1].month, 'day' : weeks[i+1].day})

        return JsonResponse(ret, status=200, safe=False)
    return JsonResponse({}, status=200)

''' Gets all the unique requests made in the past 5 weeks. It doesn't filter based off location. Works same way as getUniqRequests
    but doesnt take into account location of user who made the query.
    @request is http get request sent by dashboard.js. 
    @returns json response containing an array of {value : no of users, year : year, month: month, day : day} dicts. 
            This format makes it easy for dashboard.js to process this data. There are 5 dicts for the last 5 weeks.'''
def getAllUniqRequests(request):

    if request.method == 'GET':
        today = datetime.datetime.now()
        weeks = calculateDates(today)
        ret = []
        for i in range(len(weeks) - 1):
            seen = []
            count = 0
            queries = QUERY_TABLE.objects.filter(date__range=[weeks[i], weeks[i+1]])
            for query in queries:
                if query.search_term in seen:
                    continue
                seen.append(query.search_term)
                count += 1
            ret.append({'value' : count, 'year' : weeks[i+1].year, 'month' : weeks[i+1].month, 'day' : weeks[i+1].day})

        return JsonResponse(ret, status=200, safe=False)
    return JsonResponse({}, status=200)


def getTableData(request):
    if request.is_ajax and request.method == 'GET':
        location = int(request.GET.get('location'))
        table = request.GET.get('table')

        json = {str(time) : load_query_data_with_parameters(table, loc=location, time=time) for time in range(1,4)}

        return JsonResponse(json, status=200, safe=False)
    return JsonResponse({}, status=200)


# NOTE - I think once our django database is deployed we won't need the csrf_exempt tags. 

'''Used by the desktop application to check if the email in request is already in our database. 
   @param request http post request sent by desktop application. Contains an email.
   @returns Exists if there is a row with same email that is sent else it sends Does not exist.'''
@csrf_exempt
def checkEmail(request):
    
    if request.method == 'POST':
        email = request.POST.get('email')
        try:
            check = User.objects.filter(email=email)
            if len(check) == 0:
                return JsonResponse({'message' : 'Does not exist'}, status=200, safe=False)
            return JsonResponse({'message' : 'Exists'}, status=200, safe=False)
        except User.DoesNotExist:
            return JsonResponse({'message' : 'Does not exist'}, status=200, safe=False)

'''Adds a user to the user table. 
   @param request is a http post request sent by desktop app. it contains the email and location of user. Location may be N/A if user declines to
        to share location.
   @returns Success if we succesfully added user to table. If user already exists we sent Already user created.'''
@csrf_exempt
def addUser(request):

    if request.method == 'POST':
        email = request.POST.get('email')
        try:
            location = int(request.POST.get('location'))
        except ValueError:
            return JsonResponse({'message' : 'Invalid location'}, status=200, safe=False)
        if len(User.objects.filter(email=email)) > 0:
            return JsonResponse({"message" : "User already created."}, status=200, safe=False)

        user = User(email=email, location=location)
        user.save()
        return JsonResponse({"message" : "Success"}, status=200, safe=False)

'''Saves the url sent in the request to the ClickedUrl table. 
   @param request is a http post request sent by desktop app. The request contains email of user who clicked the url and the whitelisted url.
        the url is also of the whitelist not actual website e.g. https://gpnotebook.com/simplepage.cfm?ID=x20050123201337159860 would just return
        https://gpnotebook.com
    @returns Success if we succesfully added url otherwise it returns unsuccessful.'''
@csrf_exempt
def saveUrl(request):

    if request.method == 'POST':
        email = request.POST.get('email')
        site = request.POST.get('url')

        user = User.objects.filter(email=email)
        if(len(user) == 0):
            return JsonResponse({'message' : 'User not found'}, status=200, safe=False)

        final_url = ClickedUrl(url=site, user=user[0])
        final_url.save()

        return JsonResponse({"message" : "Success"}, status=200, safe=False)
    return JsonResponse({"message" : "Unsuccessful"}, status=200, safe=False)

'''Saves a query to the Query database. 
   @param request contains email and query which we use to create a query that is saved
   @returns Success if we managed to save query successfully otherwise it returns unsuccesful'''
@csrf_exempt
def saveQuery(request):

    if request.method == 'POST':
        email = request.POST.get('email')
        query = request.POST.get('query')

        user = User.objects.filter(email=email)
        if(len(user) == 0):
            return JsonResponse({'message' : 'User not found'}, status=200, safe=False)

        final_query = Query(search_term=query, user=user[0]) 
        final_query.save()
        return JsonResponse({"message" : "Success"}, status=200, safe=False)
    return JsonResponse({"message" : "Unsuccessful"}, status=200, safe=False)

'''Method used for cleaning up after tests testing AddUser.'''
@csrf_exempt
def deleteUser(request):

    if request.method == 'POST':
        email = request.POST.get('email')
        user = User.objects.filter(email=email)
        if (len(user) == 0):
            return JsonResponse({'message' : 'User does not exist'}, status=200, safe=False)
        
        user[0].delete()
        return JsonResponse({'message' : 'Success'}, status=200, safe=False)
    return JsonResponse({"message" : "Unsuccessful"}, status=200, safe=False)

'''Used to clean up after testing SaveQuery.'''
@csrf_exempt
def deleteQuery(request):

    if request.method == 'POST':
        email = request.POST.get('email')
        query = request.POST.get('query')

        user = User.objects.filter(email=email)
        if (len(user) == 0):
            return JsonResponse({'message' : 'User not found'}, status=200, safe=False)
        
        q = Query.objects.filter(search_term=query, user=user[0])
        q.delete()
        return JsonResponse({"message" : "Success"}, status=200, safe=False)
    return JsonResponse({"message" : "Unsuccessful"}, status=200, safe=False)

'''Used to clean up after testing SaveUrl'''
@csrf_exempt
def deleteUrl(request):

    if request.method == 'POST':
        email = request.POST.get('email')
        url = request.POST.get('url')

        user = User.objects.filter(email=email)
        if (len(user) == 0):
            return JsonResponse({'message' : 'User not found'}, status=200, safe=False)

        q = ClickedUrl.objects.filter(url=url, user=user[0])
        q.delete()
        return JsonResponse({"message" : "Success"}, status=200, safe=False)
    return JsonResponse({"message" : "Unsuccessful"}, status=200, safe=False)

@csrf_exempt
def getRecommendations(request):
    if request.method == 'POST':
        link = request.POST.get('link')
        ret = Recommendation.objects.filter(url=link)
        if len(ret) == 0:
            return JsonResponse({'message' : 'Unsuccessful'}, status=200, safe=False)

        return JsonResponse({"message" : "Success", "content":ret[0].count},status=200, safe=False)
    return JsonResponse({"message": "Unsuccessful"}, status=200, safe=False)

@csrf_exempt
def saveRecommendations(request):
    if request.method == 'POST':
        link = request.POST.get('link')
        
        try:
            count = int(request.POST.get('recommendationCount'))
        except ValueError:
            return JsonResponse({"message": "Unsuccessful"}, status=200, safe=False)
        
        check = Recommendation.objects.filter(url=link)
        if len(check) == 1:
            check[0].count = count
            check[0].save()
            return JsonResponse({"message" : "Success"}, status=200, safe=False)

        save = Recommendation(url=link, count=count)
        save.save()
        return JsonResponse({"message" : "Success"}, status=200, safe=False)
    return JsonResponse({"message": "Unsuccessful"}, status=200, safe=False)

'''Helper function to for testing saveRecommendation and getRecommendation.'''
@csrf_exempt
def deleteRecommendation(request):

    if request.method == 'POST':
        link = request.POST.get('link')
        check = Recommendation.objects.filter(url=link)
        if len(check) == 0:
            return JsonResponse({"message": "Unsuccessful"}, status=200, safe=False)

        check[0].delete()
        return JsonResponse({"message" : "Success"}, status=200, safe=False)
    return JsonResponse({"message": "Unsuccessful"}, status=200, safe=False)