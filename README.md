# Consultation+ : a simple tool to make GP's lives easier
This project is licensed under the AGPLv3 License.

![image](https://user-images.githubusercontent.com/55798132/112853780-1ae2b200-90a5-11eb-8404-d2399221e52f.png)

## Table of Contents

* [Overview](#overview)

* [Technologies](#technologies)

* [Architecture](#architecture)

* [Privacy and Security](#privacy-and-security)

* [Downloads](#downloads)

* [Developer](#developer)



## Overview

Many GPs spend far too much time routing through many different untrustworthy sources of information online. This hampers their ability to see patients efficiently. We seek to change this.

Consultation+ searches the internet and only returns websites from trusted sources, enabling the GPs workflow.

Furthermore data about search queries is also collected and collated into insights displayed on a django dashboard, allowing trends across the country to be found. This data also makes the app easily extensible as an endpoint can be created, letting even more people use and transform the great amount of data being collected.

The information displayed on the dashboard will be useful not only to GPs but anyone interested in seeing general search trends across the country. 

**The User manual can be found in the "Key Files" folder to help you learn how to get started with the application.**

## Technologies
Vastly different technologies were used between the two parts of the project:

### Desktop App
* Built using the Microsoft ```.NET``` framework in C#
* Uses the Google CustomSearch API to search data
* Authentication carried out using Microsoft Graphs
* Stored in the ```ConsultationPlus1``` folder in this repo
* Makes use of the Microsoft Graphs API to implement SSO and saving of history and settings to users OneDrive. 

Initial View             |  Search View
:-------------------------:|:-------------------------:
![Screenshot (103).png](https://github.com/jklubienski/SysEng-Team-33/blob/main/images/Screenshot%20(103).png?raw=true)  |  ![Screenshot (104).png](https://github.com/jklubienski/SysEng-Team-33/blob/main/images/Screenshot%20(104).png?raw=true)

### Dashboard Webapp
* Built using Django and Python
* NLP carried out using NLTK to link similar insights together
* Data was stored in a SQLite database in testing but has moved to PostgreSQL in production
* Available in the ```Dashboard``` folder in this repo

![Better Dashboard.png](https://github.com/jklubienski/SysEng-Team-33/blob/main/images/Better%20Dashboard.png?raw=true)

## Architecture
A detailed look at the components of the system, how they interact, and what type of data they interact with.

![ArchitectureDiagram.png](https://github.com/jklubienski/SysEng-Team-33/blob/main/images/ArchitectureDiagram.png?raw=true)

## Privacy and Security
Our [team](consultationplus.herokuapp.com) has made every effort to be compliant with the GDPR regulations on privacy.

* Data is never stored on a public database without explicit permission from the user
* All personal Data is stored locally in OneDrive 
* Microsoft accounts are used for authentication so no passwords are stored anywhere in the system
* The graphs API uses Read/Write.AppFolder permission. This permission only allows our application to read and write to files in our special application folder. No other files or folders can be accessed.

## Downloads
* A current build of the desktop app can be found [here](consultationplus.herokuapp.com) 
* The dashboard has already been deployed as a heroku application (Available at [consultationplus.herokuapp.com](consultationplus.herokuapp.com))

## Developer
Contains the relevant processes needed to run and alter both parts of this project. 

### Desktop App
* Firstly, you must set up the Microsoft Graphs and Google Custom Search Engine (CSE) APIs. 
* To set up the Graphs API, you must head to the Azure active directory and register our application. Make sure to add the following permissions: email, User.Read and Files.ReadWrite.AppFolder. Once the application and the permissions are set up, paste the client ID into the clientId variable in ```GraphsAPIHandler.cs```.
* To set up the CSE API, head to the Google custom search API control panel. Once you reach there, create a new search engine and in the section "sites to search" add all of the sites in the ```whitelist.txt``` file. Once that is done paste the search engine ID and api key into the variables cx and apiKey in ```GoogleAPIHandler.cs``` Note: the free version of the CSE API only allows 100 queries per day.
* Finally, when running the application make sure to have the server running. If you are using the local Django version use the command ```python manage.py runserver```.
* Now I will outline the file structure in the desktop application so you know basics of how the files interact with each other:
	* ```Program.cs``` contains the main function which is called first. It also contains small classes used to hold various pieces of information. You can find it in ConsultationPlus1/WindowsFormsApp2.
	* ```GraphsAPIHandler.cs``` deals with the Microsoft Graph API. This class is quite interlinked with other files. It's main job is to handle SSO and save categories,privacy and history to OneDrive and load categories and privacy from OneDrive. You can find it in ConsultationPlus1/WindowsFormsApp2.
	* ```GoogleAPIHandler.cs```is responsible for searching. It has only one public function and the rest are auxiliary functions to help it filter the results, build the query and send the http request.  You can find it in ConsultationPlus1/WindowsFormsApp2.
	* ```DatabaseCommunicator.cs``` handles all sending and receiving of data from the django database by using HTTP requests. You should view ```urls.py``` and ```views.py``` while reading the code for this class to learn exactly how it works. You can find ```DatabaseCommunicator.cs``` in ConsultationPlus1/WindowsFormsApp2. ```views.py``` and ```urls.py``` can be found in dashboard/consultation_plus.
	* The Forms folder contains all the forms for the UI. Each form apart from ```SearchForm.cs``` contains small bits of logic related to that form. ```SearchForm.cs``` is quite lengthy and handles all the logic related to the main Search UI. All of the forms are found in ConsultationPlus1/WindowsFormsApp2/Forms.
	* Finally, you can view the the tests in the ```UnitTest``` and ```IntegrationTests``` folders. We could only unit test the functions in ```Program.cs``` but we were able to perform integration tests on ```DatabaseCommunicator.cs``` as well as ```GoogleAPIHandler.cs```. ```GraphsAPIHandler.cs``` is too interlinked with other classes to integration test.

### Django 

Now we will explain the files related to the databsaae and how they relate to each other:

* ```urls.py``` contains the urls for the http requests that are resolved to a functions contained in ```views.py```. If you wish to create new HTTP requests you must create a url here and add a function for that url in ```views.py```.
* ```views.py``` contains functions to handle HTTP requests from the dashboard and the desktop application. Each function returns a Json object with a key called "message" which specifies if the request was succesfully handled or not.
* Tests can be found in the tests folder. There are two testing files called ```test_urls.py``` and ```test_views.py```. ```test_urls.py``` tests if each url is resolved to the correct function in ```views.py```. ```test_views.py``` tests if the functions in ```views.py``` return the correct data. You can run these tests by opening a command line in the ```consultation_plus``` folder and running ```python manage.py test app```.

### Dashboard 
* Open the terminal and ```cd``` into the ```consultation_plus``` folder
* Install requirements by running: ```pip install -r requirements.txt```
* Run the server: ```python manage.py runserver```
* Go to the local ip specified in the terminal (probably ```127.0.0.1:8000```) to view the content.
* Now we will briefly explain the files related to the dashboard:
	* ```index.html``` contains the html code that was used for the dashboard. The html code was generated using WebFlow but we have added embedded JS code for the geographical map.
	* ```dashboard.js``` contains code related to the drawing of the graphs on the dashboard. The library we used is D3.js and we fetch the data from the Django server using AJAX requests.

