# GrowGreen Project

## Description
The Singapore Green Plan 2030 is a national sustainability movement which seeks to rally bold and collective action to tackle climate change. Climate change is a global challenge, and Singapore is taking firm actions to do our part to build a sustainable future.

**GrowGreen** is a web application that aims to increase participation in the 3R's (Reduce, Reuse, Recycle) with an emphasis on 'Reduce'.

## Team members
* Viona Erika **(Leader)**
* Ashlee Tan
* Ong Ruey How
* Mohamed Irfan

## Features
* Viona Erika - Course Management, LMS with video uploads, AWS S3, AWS Transcribe
* Ashlee Tan - Recycler (find nearest recycling point with user-contributed data), AWS Rekognition (Cloud ML Image Analysis), Chat, Newsletters

## Set-up guide
This guide assumes you already have SQL Server Management Studio (SSMS) installed, and access to an instance of SQL Server.

1. Firstly, import the database script using SSMS. This will create a new database called 'GrowGreen'. 
   
   (Delete any existing databases if necessary)

2. Open Terminal (MacOS/Linux) or PowerShell (Windows).

3. cd to GrowGreen/GrowGreenWeb (project directory).

4. Then type these commands (with Visual Studio closed):

```powershell
dotnet tool install --global dotnet-ef --version 6.*
dotnet restore
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:GrowGreenDB "Server=<server_ip_or_localhost_here>; Database=GrowGreen; User Id=<server_user_id_here>; Password=<server_user_password_here>"  # if you are using SQL Server Authentication
dotnet user-secrets set ConnectionStrings:GrowGreenDB "Server=<server_ip_or_localhost_here>; Database=GrowGreen; Integrated Security=true"  # if you are using Windows Authentication
dotnet ef dbcontext scaffold Name=ConnectionStrings:GrowGreenDB Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --force
```
