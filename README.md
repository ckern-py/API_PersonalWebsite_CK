# API_PersonalWebsite_CK
This is a repo containing an Azure API I created to help support my website.

## Description
This repo contains an Azure API I created to help support my website. The API currently does two things, it tracks page visits and serves information to my website so that it can be displayed. Page visits are tracked and stored in a database so that I can gets stats over a long period of time. The API also serves information to my website, like the list of GitHub projects, so that it can easily be displayed. Storing the information in a database allows me to easily update it and allows changes to be displayed instantly. I plan to keep updating this API and add more functionality soon. 

## Install
Installing this application on your local machine should be simple. You need to make sure you have NET Core Version 8.0 installed. Then you can clone the repo in Visual Studio and open the solution file. 

## Use
This project is intended to be hosted and ran in Azure with the supporting infrastructure. You can run it locally for debugging or as a one off if needed. To run in Azure, you will need some infrastructure set up with it. You'll need the API hosted. Azure has a few different options, so you can choose the one you are most familiar with. After choosing how the API is hosted, you'll also need application insights and an Azure SQL Database at the very minimum. With it being hosted in Azure it will always be available for you to call. You won't have to do anything, and information will automatically be written to the database, when it's received. 

## License
[GNU GPLv3](https://choosealicense.com/licenses/gpl-3.0/)
