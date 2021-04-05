from django.test import SimpleTestCase
from django.urls import reverse, resolve
from app.views import (
    getRequests,
    getNewUsers,
    getUniqRequests,
    getAllNewUsers,
    getAllRequests,
    getAllUniqRequests,
    checkEmail,
    addUser,
    saveUrl,
    saveQuery,
    getTableData,
    getRecommendations,
    saveRecommendations,
    deleteUser,
    deleteUrl,
    deleteQuery
    )

class TestUrls(SimpleTestCase):

    def test_get_requests_url_resolves(self):
        url = reverse('get_requests')
        self.assertEquals(resolve(url).func, getRequests)
        
    def test_get_new_users_url_resolves(self):
        url = reverse('get_new_users')
        self.assertEquals(resolve(url).func, getNewUsers)

    def test_get_uniq_requests_url_resolves(self):
        url = reverse('get_uniq_requests')
        self.assertEquals(resolve(url).func, getUniqRequests)

    def test_get_all_new_users_url_resolves(self):
        url = reverse('get_all_new_users')
        self.assertEquals(resolve(url).func, getAllNewUsers)

    def test_get_all_requests_url_resolves(self):
        url = reverse('get_all_requests')
        self.assertEquals(resolve(url).func, getAllRequests)

    def test_get_all_uniq_requests_url_resolves(self):
        url = reverse('get_all_uniq_requests')
        self.assertEquals(resolve(url).func, getAllUniqRequests)
    
    def test_get_table_data_resolves(self):
        url = reverse('get_table_data')
        self.assertEquals(resolve(url).func, getTableData)

    def test_check_email_url_resolves(self):
        url = reverse('check_email')
        self.assertEquals(resolve(url).func, checkEmail)

    def test_add_user_url_resolves(self):
        url = reverse('add_user')
        self.assertEquals(resolve(url).func, addUser)

    def test_save_url_url_resolves(self):
        url = reverse('save_url')
        self.assertEquals(resolve(url).func, saveUrl)

    def test_save_query_url_resolves(self):
        url = reverse('save_query')
        self.assertEquals(resolve(url).func, saveQuery)

    def test_get_recommendations_resolves(self):
        url = reverse('get_recommendations')
        self.assertEquals(resolve(url).func, getRecommendations)

    def test_save_recommendations_resolves(self):
        url = reverse('save_recommendations')
        self.assertEquals(resolve(url).func, saveRecommendations)

    def test_delete_user_resolves(self):
        url = reverse('delete_user')
        self.assertEquals(resolve(url).func, deleteUser)

    def test_delete_query_resolves(self):
        url = reverse('delete_query')
        self.assertEquals(resolve(url).func, deleteQuery)

    def test_delete_url_resolves(self):
        url = reverse('delete_url')
        self.assertEquals(resolve(url).func, deleteUrl)
