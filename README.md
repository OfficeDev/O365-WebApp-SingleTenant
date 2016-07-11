Office 365 APIs Single Tenant Web Application
==============================================

This sample shows how to build a single tenant MVC web application that uses Azure AD for sign-in using the OpenID Connect protocol, and then calls a Office 365 API under the signed-in user's identity using tokens obtained via OAuth 2.0. This sample uses the OpenID Connect ASP.Net OWIN middleware and ADAL .Net.

***Update 12/16/2014***
The sample now uses a persistent ADAL token cache that uses a database for its token cache. You can see the token cache implementation in the following files:
- [Models/ADALTokenCache.cs](https://github.com/OfficeDev/O365-WebApp-SingleTenant/blob/master/O365-WebApp-SingleTenant/Models/ADALTokenCache.cs)
- [Models/ApplicationDbContext.cs](https://github.com/OfficeDev/O365-WebApp-SingleTenant/blob/master/O365-WebApp-SingleTenant/Models/ApplicationDbContext.cs)

## How to Run this Sample
To run this sample, you need:

1. Visual Studio 2013
2. [Office Developer Tools for Visual Studio 2013](http://aka.ms/OfficeDevToolsForVS2013)
3. Office 365 Developer Subscription. [Join the Office 365 Developer Program and get a free 1 year subscription to Office 365](https://profile.microsoft.com/RegSysProfileCenter/wizardnp.aspx?wizid=14b845d0-938c-45af-b061-f798fbb4d170&lcid=1033)

## Step 1: Clone or download this repository
From your Git Shell or command line:

`git clone https://github.com/OfficeDev/O365-WebApp-SingleTenant.git`

## Step 2: Build the Project
1. Open the project in Visual Studio 2013.
2. Simply Build the project to restore NuGet packages.
3. Ignore any build errors for now as we will configure the project in the next steps.

## Step 3: Configure the sample
Once downloaded, open the sample in Visual Studio.

### Register Azure AD application to consume Office 365 APIs
Office 365 applications use Azure Active Directory (Azure AD) to authenticate and authorize users and applications respectively. All users, application registrations, permissions are stored in Azure AD.

Using the Office 365 API Tool for Visual Studio you can configure your web application to consume Office 365 APIs.

1. In the Solution Explorer window, **right click your project -> Add -> Connected Service**.
2. A Services Manager dialog box will appear. Choose **Office 365 -> Office 365 API** and click **Register your app**.
3. On the sign-in dialog box, enter the username and password for your Office 365 tenant.
4. After you're signed in, you will see a list of all the services.
5. Initially, no permissions will be selected, as the app is not registered to consume any services yet.
6. Select **Users and Groups** and then click **Permissions**
7. In the **Users and Groups Permissions** dialog, select **Enable sign-on and read users profiles'** and click **Apply**
8. Select **Contacts** and then click **Permissions**
9. In the **Contacts Permissions** dialog, select **Read users' contacts** and click **Apply**
10. Click **Ok**

After clicking OK, Office 365 client libraries (in the form of NuGet packages) for connecting to Office 365 APIs will be added to your project.

In this process, Office 365 API tool registered an Azure AD Application in the Office 365 tenant that you signed in the wizard and added the Azure AD application details to web.config.

### Step 4: Update web.config with your Tenant ID
There is one extra configuration required if you are building a single tenant application.

In your web.config, update the **TenantId** value to your **Office 365 tenant Id** where the application is deployed.

To get the tenant Id of your Office 365 tenant:
- Log in to your Azure Portal and select your Office 365 domain directory.

**NOTE:** If you are unable to login to [Azure Portal](https://manage.windowsazure.com) using your Office 365 credentials, You can also access your Office 365’s Azure Portal directly from your [Office 365 Admin Center](http://chakkaradeep.com/index.php/access-azure-active-directory-portal-from-your-office-365-subscription/)

- Now, in the browser URL, locate the GUID. This will be your Office 365 tenant Id.
- Copy and paste it in the web.config where it says “paste-your-tenant-guid-here“ : 
<add key=“ida:TenantId“ value=“paste-your-tenant-guid-here“ />

**Note:** If you are deploying to a production tenant, you will need to ask your tenant admin for the tenant identifier.

### Step 5: Build and Debug your web application
Now you are ready for a test run. Hit F5 to test the app.

### Quick Look at the Authentication Code
The authentication startup class, **App_Start/Startup.Auth.cs** in the project contains the startup logic for Azure AD authentication.

The sample now uses a persistent ADAL token cache that uses a database for its token cache. You can see the token cache implementation in the following files:
- [Models/ADALTokenCache.cs](https://github.com/OfficeDev/O365-WebApp-SingleTenant/blob/master/O365-WebApp-SingleTenant/Models/ADALTokenCache.cs)
- [Models/ApplicationDbContext.cs](https://github.com/OfficeDev/O365-WebApp-SingleTenant/blob/master/O365-WebApp-SingleTenant/Models/ApplicationDbContext.cs)


### Sign In and Sign Out Controls
The sign in and sign out controls are already added to the views. You can find them under **Views\Shared** folder.
1. **_LoginPartial.cshtml** is the partial view that renders the Sign In and Sign Out actions.
2. **_LoginPartial.cshtml** is then rendered in _Layout.cshtml
3. The **AccountController.cs** has the required methods for sign in and sign out.

### Requiring authentication to access controllers
Applying **Authorize** attribute to all controllers in your project will require the user to be authenticated before accessing these controllers. To allow the controller to be accessed anonymously, remove this attribute from the controller. If you want to set the permissions at a more granular level, apply the attribute to each method that requires authorization instead of applying it to the controller class.

### Write Code to call Office 365 APIs
You can now write code to call an Office 365 API in your web application. You can apply the Autorize attribute to the desired controller or the method in which you wish to call Office 365 API.

**ContactsController.cs** describes how to interact with the Office 365 API discovery service, get the endpoint URI and resource Id for Outlook Services and then query users' contacts.
