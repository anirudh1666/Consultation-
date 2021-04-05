from django.urls import path
from . import views

urlpatterns = [
    path('', views.index, name='app-index'),
    path('get/ajax/get/requests', views.getRequests, name='get_requests'),
    path('get/ajax/get/new/users', views.getNewUsers, name='get_new_users'),
    path('get/ajax/get/uniq/requests', views.getUniqRequests, name='get_uniq_requests'),
    path('get/ajax/all/new/users', views.getAllNewUsers, name='get_all_new_users'),
    path('get/ajax/all/requests', views.getAllRequests, name='get_all_requests'),
    path('get/ajax/all/uniq/requests', views.getAllUniqRequests, name="get_all_uniq_requests"),
    path('get/ajax/get/table/data', views.getTableData, name="get_table_data" ),
    path('post/app/check/email', views.checkEmail, name='check_email'),
    path('post/app/add/user', views.addUser, name='add_user'),
    path('post/app/save/url', views.saveUrl, name='save_url'),
    path('post/app/save/query', views.saveQuery, name='save_query'),
    path('post/app/delete/user', views.deleteUser, name='delete_user'),
    path('post/app/delete/query', views.deleteQuery, name='delete_query'),
    path('post/app/delete/url', views.deleteUrl, name='delete_url'),
    path('post/app/save/recommendations', views.saveRecommendations, name='save_recommendations'),
    path('post/app/get/recommendations', views.getRecommendations, name='get_recommendations'),
    path('post/app/delete/recommendation', views.deleteRecommendation, name='delete_recommendation')
]
