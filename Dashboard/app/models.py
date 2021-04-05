from django.db import models


class User(models.Model):
    email = models.EmailField(max_length=30, unique=True)
    location = models.IntegerField()
    date_registered = models.DateTimeField(auto_now_add = True)


class Query(models.Model):
    search_term = models.CharField(max_length = 100)
    date = models.DateTimeField(auto_now_add = True) #Adds the current date to the field only when the object is created 
    user = models.ForeignKey(User, default=1, on_delete = models.CASCADE) #If the user is deleted all the queries are as well

    def __str__(self):
        return self.search_term

class ClickedUrl(models.Model):
    url = models.URLField()
    date = models.DateTimeField(auto_now_add = True)
    user = models.ForeignKey(User, default=1, on_delete = models.CASCADE)

#Classes used to create dummy data
class TestUser(models.Model):
    email = models.EmailField(max_length=30, unique=True)
    location = models.IntegerField()
    date_registered = models.DateTimeField(auto_now_add = False)

class TestQuery(models.Model):
    search_term = models.CharField(max_length = 100)
    date = models.DateTimeField(auto_now_add = False)
    user = models.ForeignKey(TestUser, default=1, on_delete = models.CASCADE) 

class TestUrl(models.Model):
    url = models.URLField()
    title = models.CharField(max_length=100)
    date = models.DateTimeField(auto_now_add = True)
    user = models.ForeignKey(TestUser, default=1, on_delete = models.CASCADE)

class Recommendation(models.Model):
    url = models.URLField()
    count = models.IntegerField(default = 0, null = True)
    
'''
Note - to add queries for testing

run 'python manage.py shell'

from app.models import Query
from django.contrib.auth.models import User
user = User.objects.filter(username = "James-Admin").first()
query = Query(search_term = "CKD", user_id = user.id)
query.save()
'''