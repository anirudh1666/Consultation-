#Refreshes the app's lists of data every week and conducts NLP on the data 
import nltk, datetime, json, os
from nltk.corpus import wordnet, stopwords
from nltk.tokenize import word_tokenize
from .models import TestQuery, Query, ClickedUrl
from django.conf import settings
from dateutil.parser import parse as date_parse
# from celery.task.schedules import crontab
# from celery.decorators import periodic_task

JSON_PATH = os.path.join(settings.BASE_DIR, 'app/static/app/data/table_data.json')
QUERY_TABLE = TestQuery

#Removes stopwords and adds underscores to be in the correct format for wordnet
def sanitise_data(data:str):
    tokens = word_tokenize(data)
    remove_stopwords = [word for word in tokens if not word in stopwords.words()]
    return "_".join(remove_stopwords)


def cluster_data(data, table):
    return_data = {}
    generated = [] #Array of indexes that have already been matched

    for i, word_type in enumerate(data):
        if i not in generated:
            word, word_loc, word_time = word_type
            generated.append(i)
            #Return data is presented in the form (number of matches, [Users that have made those queries]) 
            # - The user array is used for discering the location of the data
            return_data[word] = {"count":1, "location":[word_loc], "date":[word_time]}

            for j, syn_type in enumerate(data):
                #Checks if word has been clustered already and if not matches it to synonyms 
                synonymn, syn_loc, syn_time = syn_type
                if (j not in generated 
                and ((synonymn in [lemma.name() for syn in wordnet.synsets(word) for lemma in syn.lemmas()] and table == "query")
                or (synonymn == word and table == "url"))
                ):
                    generated.append(j)
                    return_data[word]["count"] += 1
                    return_data[word]["location"].append(syn_loc)
                    return_data[word]["date"].append(syn_time)

    #Returns all terms in descending order of their matches 
    return dict(sorted(return_data.items(), key = lambda x: x[1]["count"], reverse=True))

def convert_datetime(obj):
    if isinstance(obj, datetime.datetime):
        return obj.__str__()

#Celery function, acts every week at 3AM on a Monday, commented out while running locally
#@periodic_task(run_every=(crontab(hour=3, minute=00, day_of_week=1)), name="compute_all_data", ignore_result=True)
def compute_all_data():
    unclustered_query_data = [(sanitise_data(query.search_term), query.user.location, query.date) for query in TestQuery.objects.all()]
    query_data = cluster_data(unclustered_query_data, "query")

    unsorted_url_data = [(url.url, url.user.location, url.date) for url in ClickedUrl.objects.all()]
    url_data = cluster_data(unsorted_url_data, "url")

    #JSONifys the data 
    with open(JSON_PATH,'w') as table_data:
        serialised_json = json.dumps({"query":query_data, "url":url_data}, default=convert_datetime)
        table_data.write(serialised_json)

#Function called to load specific data from specific locations/times 
def load_query_data_with_parameters(table, loc=0, time=3):
    with open(JSON_PATH,'r') as table_data:
        data = table_data.read()
    
    queries = json.loads(data)[table]
    time_set, result_set = {},{}

    check_valid_date = lambda x, q: (datetime.datetime.now() - datetime.timedelta(days=x)).timestamp() < q.timestamp()

    for query_name, query_args in queries.items():
        #Filter for time. time=3 denotes all results 

        if time == 3:
            time_set = queries.copy()
            break
        else:
            match_array = []
            for i in range(query_args["count"]):
                if time == 1 and check_valid_date(7,date_parse(query_args["date"][i])): #Denotes results for this week
                    match_array.append(query_args["location"][i])
                elif time == 2 and check_valid_date(31,date_parse(query_args["date"][i])): #Denotes results for this month
                    match_array.append(query_args["location"][i])

            if len(match_array) != 0:
                time_set[query_name] = {"count":len(match_array), "location":match_array}

    #Filter for location
    for query_name, query_args in time_set.items():
        if loc == 0:
            result_set = time_set.copy()
            break
        matches = 0
        for i in range(query_args["count"]):
            if query_args["location"][i] == loc:
                matches += 1
        if matches != 0:        
            result_set[query_name] = {"count": matches}
        
    #Only returns the top 10 items 
    return dict(list(result_set.items())[:10])