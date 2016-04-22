.. _manual_test_descriptions:

============
Manual Tests
============

.. contents:: Table of Contents

Test Preparations
-----------------

Hardware Preparations
#####################

You need a computer with access to the UAHealth server.

Software Preparations
#####################

You need the latest Chrome or Firefox version installed on the computer.

Other Preparations
##################

It is expected that you will have run the automated tests before executing these tests. Some of the users and data
depend on the automation have run correctly.

Test Details
------------

Multiple File Upload Test
#########################

**Requirements:**

- 3.1.2.1: The system shall process multiple files at the same time

**Pre Conditions:**

- You must be logged in as the testPatient user using these credentials

 - Username: 'testPatient'
 - Password: 'P@ssword10'

- You must have access to the data files in the project folder here: ./med656/Tests/Data

**Input:**

**Test Procedure:**

===========================================================================  ===========================================  =============
Steps                                                                        Expected Result                              Actual Result
===========================================================================  ===========================================  =============
click the 'Upload Data' link in top right corner of page                     Taken to the upload page
click the 'BROWSE' button                                                    Windows file select wizard opens
navigate to ./med656/Tests/Data/Zephyr/ZephyrTestData/                       Files are visible
highlight the ...Accel.csv and ...BB.csv files by holding ctrl and clicking  Files are highlighted
click the 'chose' button on the wizard                                       File names are in the upload page now
fill in date 6-24-2015 for both from date and to date                        Dates are visible on the page
click on zephyr radio button                                                 Radio button is highlighted
click "SUBMIT" button                                                        No errors are displayed
===========================================================================  ===========================================  =============

User Privacy Test
#################

**Requirements:**

- 3.1.1.1.1.2: The system shall prevent any personal identifiable information from being available for a Patient

**Pre Conditions:**

- The user must be logged off
- A physician named 'testPhysician' must be created

 - To check if this user is created login as user 'fitadmin' password 'Password1!'
 - Click the 'Manage Accounts' button at the top of the page
 - Select 'Physicians' from the Roles dropdown and look for 'testPhysician'

- If physician 'testPhysician' does not exist

 - Follow the steps here (:ref:`create_physician_account`) to create a physician account with the following information

  - first name = test
  - last name = test
  - username = testPhysician
  - password = 'P@ssword10'
  - email = testphysician@test.com
  - address = here
  - phone number = 123-456-7890
  - reason = test

**Input:**

- user = 'testPatientPrivacy'
- password = 'P@ssword10'

**Test Procedure:**

========================================================================  ===========================================  =============
Steps                                                                     Expected Result                              Actual Result
========================================================================  ===========================================  =============
login as 'testPhysician' with password 'P@ssword10'                       Login Success!
click 'create patient' button                                             Taken to the create patient page
verify that none of the information requested is personally identifiable  None of the info is personally identifiable
========================================================================  ===========================================  =============

Physician Graph View
####################


**Requirements:**

- 3.1.1.1.2.3: The system shall allow the physician user to view their patient’s data graphically.
- 3.1.7: The system shall provide a user interface for displaying medical data in the system.
- 3.1.7.16: The system shall display summary heart rate data on a chart from the Zephyr and BasisPeak.
- 3.1.7.16.1: The chart showing summary heart rate data from the Zephyr and BasisPeak will also show heart rate data from the Microsoft Band.

**Pre Conditions:**

- The user must be logged off
- A physician named 'testPhysician' must be created

 - To check if this user is created login as user 'fitadmin' password 'Password1!'
 - Click the 'Manage Accounts' button at the top of the page
 - Select 'Physicians' from the Roles dropdown and look for 'testPhysician'

- If physician 'testPhysician' does not exist

 - Follow the steps here :ref:`create_physician_account` to create a physician account with the following information

  - first name = test
  - last name = test
  - username = testPhysician
  - password = 'P@ssword10'
  - email = testphysician@test.com
  - address = here
  - phone number = 123-456-7890
  - reason = test

- 'testPhysician' must have a patient 'testPatientGraphView'

 - If patient 'testPatient' does not exist

  - Follow the steps here :ref:`create_patient_account` to create patient 'testPatientGraphView' with to following information

   - user = 'testPatient'
   - pwd = 'P@ssword10'
   - birthday = '3 March, 1954'
   - location = 'Alabama'
   - weight = '200'
   - height = '72'
   - gender = 'male'
   - race = 'white'
   - ethnicity = 'non_hispanic'

**Input:**

- user = 'testPatientPrivacy'
- password = 'P@ssword10'

**Test Procedure:**

+-------------------------------------------------------------------------+--------------------------------------------+---------------+
| Steps                                                                   | Expected Result                            | Actual Result |
+=========================================================================+============================================+===============+
| Login as 'testPatient' with password 'P@ssword10'                       | Login Success!                             |               |
+-------------------------------------------------------------------------+--------------------------------------------+---------------+
| Follow user guide instructions :ref:`here <patient_data_upload>` to     | There are no errors while uploading data   |               |
| upload the following files from this folder                             |                                            |               |
| <project directory>/med656/Tests/Data/:                                 |                                            |               |
| - BasisPeak/bodymetrics_simple.csv                                      |                                            |               |
| - Zephyr/ZephyrTestData/2015_06_24__23_05_14_Summary.cvs                |                                            |               |
| - Band/data.csv                                                         |                                            |               |
+-------------------------------------------------------------------------+--------------------------------------------+---------------+
| Logout and log back in as 'testPhysician' with password 'P@ssword10'    | Login Success!                             |               |
+-------------------------------------------------------------------------+--------------------------------------------+---------------+
| Follow instructions :ref:`here <view_patient_data_physician>` to view   | The summary heart rate graph can be seen   |               |
| the 'testPatient' data that was just uploaded.                          | for the Zephyr, BasisPeak, and Band        |               |
+-------------------------------------------------------------------------+--------------------------------------------+---------------+

Database Verification Test
##########################

**Requirements:**

- 3.1.3: The system shall connect to a database
- 3.2.2: The system shall use a SQL database

**Pre Conditions**

**Inputs**

**Test Procedure**

========================================================================  ===========================================  =============
Steps                                                                     Expected Result                              Actual Result
========================================================================  ===========================================  =============
Login locally or remote in to the UAHealt Bit Vault server desktop        you are at the windows desktop
Open Microsoft SQL Database Manager                                       you can see the uahbitvault database
========================================================================  ===========================================  =============

Internet Access Verification Test
#################################

**Requirements:**

- 3.2.3: The system shall be connected to a network with internet access

**Pre Conditions**

**Inputs**

**Test Procedure**

========================================================================  ===========================================  =============
Steps                                                                     Expected Result                              Actual Result
========================================================================  ===========================================  =============
Login to the machine the system is running on                             You are at the Windows Desktop
Open a command prompt                                                     You are presented with the Windows prompt
Execute "ping www.google.com"                                             Pings respond with 100% pass
========================================================================  ===========================================  =============

OS Test
#######

**Requirements:**

- 3.2.1: The system shall run on Windows Server Operating System

**Pre Conditions:**

**Input:**

**Test Procedure:**

====================================================  ============================================  =============
Steps                                                 Expected Result                               Actual Result
====================================================  ============================================  =============
Login to the machine the system is running on         You are at the Windows Desktop
Open a command prompt                                 You are presented with the Windows prompt
Execute "systeminfo"                                  The 'OS Name:' is 'Microsoft Windows Server'
====================================================  ============================================  =============
