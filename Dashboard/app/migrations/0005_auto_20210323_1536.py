# Generated by Django 3.1.7 on 2021-03-23 15:36

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('app', '0004_auto_20210322_1715'),
    ]

    operations = [
        migrations.RenameField(
            model_name='recommendation',
            old_name='recommendationCount',
            new_name='count',
        ),
    ]