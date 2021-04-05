from django.test import TestCase, Client
from django.urls import reverse
from app.models import User, Query, ClickedUrl, TestUser, TestQuery, Recommendation
from app.views import calculateDates
import json
import datetime

'''These tests use TestQuery and TestUser since we can manually input dates for those tables whereas for
   User and Query the dates are automatically added which means that we cant test them.'''
class TestViews(TestCase):

    def setUp(self):
        self.client = Client()
        self.today = datetime.datetime.now()
        self.weeks = calculateDates(self.today)

        # Making some fake users for tests.
        # This is for the requests from desktop application.
        self.test_user1 = User(email='testRandomEmail1@testEmailFake.com', location=7)
        self.test_user1.save()
        # These are for the ajax requests.
        # registered len(weeks) - 1 weeks ago
        self.test_user2 = TestUser(email='testRandomEmail3@testEmailFake.com', location=7, date_registered=self.weeks[0] + datetime.timedelta(days = 1)) 
        self.test_user2.save()
        # registered start of 2 weeks ago
        self.test_user3 = TestUser(email='testRandomEmail4@testEmailFake.com', location=4, date_registered=self.weeks[len(self.weeks) - 3])
        self.test_user3.save()
        # registered this week.
        self.test_user4 = TestUser(email='testRandomEmail5@testEmailFake.com', location=7, date_registered=self.weeks[len(self.weeks) - 1] - datetime.timedelta(days = 3))
        self.test_user4.save()

        # Making some fake requests for tests. We will make 6 requests ranging over 2 locations and past 5 weeks.
        TestQuery(search_term="liver failure", user=self.test_user2, date=self.weeks[len(self.weeks) - 3] + datetime.timedelta(days = 3)).save()
        TestQuery(search_term="abdominal pain", user=self.test_user4, date=self.weeks[len(self.weeks) - 4] + datetime.timedelta(days = 2)).save()
        TestQuery(search_term="fractured foot", user=self.test_user2, date=self.weeks[len(self.weeks) - 2] + datetime.timedelta(days = 6)).save()
        TestQuery(search_term="liver failure", user=self.test_user2, date=self.weeks[len(self.weeks) - 3] + datetime.timedelta(days = 5)).save()
        TestQuery(search_term="hepatitis", user=self.test_user3, date=self.weeks[len(self.weeks) - 1] - datetime.timedelta(days = 2)).save()
        TestQuery(search_term="internal bleeding", user=self.test_user3, date=self.weeks[len(self.weeks) - 1] - datetime.timedelta(days = 4)).save()

        self.get_requests = reverse('get_requests')
        self.save_query = reverse('save_query')
        self.save_url = reverse('save_url')
        self.add_user = reverse('add_user')
        self.check_email = reverse('check_email')
        self.get_requests = reverse('get_requests')
        self.get_new_users = reverse('get_new_users')
        self.get_uniq_requests = reverse('get_uniq_requests')
        self.all_new_users = reverse('get_all_new_users')
        self.all_requests = reverse('get_all_requests')
        self.all_uniq_requests = reverse('get_all_uniq_requests')
        self.delete_user = reverse('delete_user')
        self.delete_query = reverse('delete_query')
        self.delete_url = reverse('delete_url')
        self.get_table_data = reverse('get_table_data')
        self.save_recommendations = reverse('save_recommendations')
        self.get_recommendations = reverse('get_recommendations')
        self.delete_recommendation = reverse('delete_recommendation')


    def test_get_requests_location_7(self):

        response = self.client.get(self.get_requests, {
            'location' : 7
        })

        data = [{'value' : 0, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 2, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]
        
        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_requests_location_4(self):

        response = self.client.get(self.get_requests, {
            'location' : 4
        })

        data = [{'value' : 0, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 2, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]
        
        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_requests_location_with_post_request(self):

        response = self.client.post(self.get_requests, {
            'location' : 7
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {})
    
    def test_get_new_users_location_7(self):

        response = self.client.get(self.get_new_users, {
            'location' : 7
        })

        data = [{'value' : 1, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_new_users_location_4(self):

        response = self.client.get(self.get_new_users, {
            'location' : 4
        })

        data = [{'value' : 0, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_new_users_location_with_post_request(self):

        response = self.client.post(self.get_new_users, {
            'location' : 7
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {})

    def test_get_uniq_requests_location_7(self):

        response = self.client.get(self.get_uniq_requests, {
            'location' : 7
        })

        data = [{'value' : 0, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_uniq_requests_location_4(self):

        response = self.client.get(self.get_uniq_requests, {
            'location' : 4
        })

        data = [{'value' : 0, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 2, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_uniq_requests_location_with_post_request(self):

        response = self.client.post(self.get_uniq_requests, {
            'location' : 7
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {})

    def test_get_all_new_users(self):

        response = self.client.get(self.all_new_users)

        data = [{'value' : 1, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]
    
        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_new_users_with_post_request(self):

        response = self.client.post(self.all_new_users)

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {})

    def test_get_all_uniq_requests(self):

        response = self.client.get(self.all_uniq_requests)

        data = [{'value' : 0, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 3, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]
    
        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_uniq_requests_with_post_request(self):

        response = self.client.post(self.all_uniq_requests)

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {})

    def test_get_all_requests(self):

        response = self.client.get(self.all_requests)

        data = [{'value' : 0, 'year' : self.weeks[len(self.weeks) - 5].year, 'month' : self.weeks[len(self.weeks) - 5].month, 'day' : self.weeks[len(self.weeks) - 5].day},
                {'value' : 0, 'year' : self.weeks[len(self.weeks) - 4].year, 'month' : self.weeks[len(self.weeks) - 4].month, 'day' : self.weeks[len(self.weeks) - 4].day},
                {'value' : 1, 'year' : self.weeks[len(self.weeks) - 3].year, 'month' : self.weeks[len(self.weeks) - 3].month, 'day' : self.weeks[len(self.weeks) - 3].day},
                {'value' : 2, 'year' : self.weeks[len(self.weeks) - 2].year, 'month' : self.weeks[len(self.weeks) - 2].month, 'day' : self.weeks[len(self.weeks) - 2].day},
                {'value' : 3, 'year' : self.weeks[len(self.weeks) - 1].year, 'month' : self.weeks[len(self.weeks) - 1].month, 'day' : self.weeks[len(self.weeks) - 1].day}]
    
        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), data)

    def test_get_all_requests_with_post_request(self):

        response = self.client.post(self.all_requests)

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {})

    def test_get_table_data(self):

        response = self.client.post(self.get_table_data)
        self.assertEqual(response.status_code, 200)
    
    '''These are all tests for requests sent from app. the app only requests data from User, Query so we dont need to worry about TestUser
       or TestQuery.'''

    def test_save_query(self):

        response = self.client.post(self.save_query, {
            'email' : 'testRandomEmail1@testEmailFake.com',
            'query' : 'kidney disease'
        })

        self.assertEqual(response.status_code, 200)
        self.assertTrue(Query.objects.filter(user=self.test_user1, search_term='kidney disease'))
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})

    def test_save_query_with_wrong_email(self):

        response = self.client.post(self.save_query, {
            'email' : 'doesNotExistEmail@testEmailFake.com',
            'query' : 'kidney failure'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'User not found'})
        self.assertFalse(Query.objects.filter(search_term='kidney failure').exists())

    def test_save_url(self):

        response = self.client.post(self.save_url, {
            'email' : 'testRandomEmail1@testEmailFake.com',
            'url' : 'www.sign.ac.uk'
        })

        self.assertEqual(response.status_code, 200)
        self.assertTrue(ClickedUrl.objects.filter(user=self.test_user1, url='www.sign.ac.uk').exists())
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})

    def test_save_url_with_wrong_email(self):

        response = self.client.post(self.save_query, {
            'email' : 'doesNotExistEmail@testEmailFake.com',
            'url' : 'gpnotebook.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'User not found'})
        self.assertFalse(ClickedUrl.objects.filter(url='gpnotebook.com').exists())
    

# test add user invalid location, test user with already created user.
    def test_add_user(self):

        response = self.client.post(self.add_user, {
            'email' : 'testRandomEmail2@testEmailFake.com',
            'location' : 6
        })

        self.assertEqual(response.status_code, 200)
        self.assertTrue(User.objects.filter(email='testRandomEmail2@testEmailFake.com', location=6).exists())
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})

    def test_add_user_with_string_location(self):

        response = self.client.post(self.add_user, {
            'email' : 'testRandomEmail9876@testEmailFake.com',
            'location' : 'string'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Invalid location'})
        self.assertFalse(User.objects.filter(email='testRandomEmail9876@testEmailFake.com').exists())

    def test_add_user_with_already_created_user(self):

        User(email='alreadyInDBUser@testEmailFake.com', location = 4).save()
        response = self.client.post(self.add_user, {
            'email' : 'alreadyInDBUser@testEmailFake.com',
            'location' : 10
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'User already created.'})

    def test_check_email(self):

        response = self.client.post(self.check_email, {
            'email' : 'testRandomEmail1@testEmailFake.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Exists'})
        self.assertTrue(User.objects.filter(email='testRandomEmail1@testEmailFake.com').exists())

    def test_check_email_wrong_email(self):

        response = self.client.post(self.check_email, {
            'email' : 'doesntExistEmail@testEmailFake.com'
        })
        
        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message': 'Does not exist'})
        self.assertFalse(User.objects.filter(email='doesntExistEmail@testEmailFake.com').exists())

    def test_delete_user(self):

        self.client.post(self.add_user, {
            'email' : 'testRandomEmail4@testFake.com',
            'location' : 12
        })
        response = self.client.post(self.delete_user, {
            'email' : 'testRandomEmail4@testFake.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})

    def test_delete_user_with_nonexistant_email(self):

        response = self.client.post(self.delete_user, {
            'email' : 'thisEmailIsNotInDatabase@randomEmail.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'User does not exist'})

    def test_delete_query(self):
        
        # cant test delete query with nonexistant user since saveQuery will fail without user.
        user = User(email='testRandomEmail11@testFake.com', location=8)
        user.save()
        self.client.post(self.save_query, {
            'email' : 'testRandomEmail11@testFake.com',
            'query' : 'broken bone'
        })
        response = self.client.post(self.delete_query, {
            'email' : 'testRandomEmail11@testFake.com',
            'query' : 'broken bone'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})
        self.assertFalse(Query.objects.filter(user=user, search_term="broken bone").exists())

    def test_delete_url(self):

        user = User(email='testRandomEmail1234@testFake.com', location=4)
        user.save()
        self.client.post(self.save_url, {
            'email' : 'testRandomEmail1234@testFake.com',
            'url' : 'gpnotebook.com'
        })
        response = self.client.post(self.delete_url, {
            'email' : 'testRandomEmail1234@testFake.com',
            'url' : 'gpnotebook.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})
        self.assertFalse(ClickedUrl.objects.filter(user=user, url='gpnotebook.com').exists())

    def test_get_recommendations(self):

        link = Recommendation(url='www.fakeurl.com', count=10)
        link.save()
        response = self.client.post(self.get_recommendations, {
            'link' : 'www.fakeurl.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success', 'content' : 10})

    def test_get_recommendations_wrong_link(self):

        response = self.client.post(self.get_recommendations, {
            'link' : 'www.fakeurl2.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Unsuccessful'})

    def test_save_recommendations_with_new_link(self):

        response = self.client.post(self.save_recommendations,  {
            'link' : 'www.fakeurl3.com',
            'recommendationCount' : '5'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})
        self.assertTrue(Recommendation.objects.filter(url='www.fakeurl3.com').exists())

    def test_save_recommendations_with_existing_url(self):

        link = Recommendation(url="www.fakeurl4.com", count=5)
        link.save()
        
        response = self.client.post(self.save_recommendations, {
            'link' : 'www.fakeurl4.com',
            'recommendationCount' : '6'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})
        self.assertTrue(Recommendation.objects.filter(url='www.fakeurl4.com', count=11).exists())

    def test_save_recommendations_with_invalid_arg(self):

        response = self.client.post(self.save_recommendations, {
            'link' : 'www.fakeurl5.com',
            'recommendationCount' : 'string'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Unsuccessful'})

    
    def test_delete_recommendation(self):

        rec = Recommendation(url="www.fakeurl6.com", count=1)
        rec.save()

        response = self.client.post(self.delete_recommendation, {
            'link' : 'www.fakeurl6.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Success'})
        self.assertFalse(Recommendation.objects.filter(url='www.fakeurl6.com').exists())

    def test_delete_recommendation_with_link_not_in_db(self):

        response = self.client.post(self.delete_recommendation, {
            'link' : 'www.fakeurl7.com'
        })

        self.assertEqual(response.status_code, 200)
        self.assertEqual(json.loads(response.content.decode('utf-8')), {'message' : 'Unsuccessful'})