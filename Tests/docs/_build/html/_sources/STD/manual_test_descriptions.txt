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

None.

Test Details
------------

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

 - Follow the steps here __'userguide link'__ to create a physician account with the following information

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

 - Follow the steps here __'userguide link'__ to create a physician account with the following information

  - first name = test
  - last name = test
  - username = testPhysician
  - password = 'P@ssword10'
  - email = testphysician@test.com
  - address = here
  - phone number = 123-456-7890
  - reason = test

- 'testPhysician' must have a patient 'testPatientGraphView'

 - If patient 'testPatientGraphView' does not exist

  - Follow the steps here ___'userguide link'___ to create patient 'testPatientGraphView' with to following information

   - user = 'testPatientGraphView'
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

========================================================================  ===========================================  =============
Steps                                                                     Expected Result                              Actual Result
========================================================================  ===========================================  =============
login as 'testPhysician' with password 'P@ssword10'                       Login Success!
========================================================================  ===========================================  =============

OS Test
#######



**Requirements:**

- 3.2.1: The system shall run on Windows Server Operating System

**Pre Conditions:**



**Input:**



**Test Procedure:**

========================================================================  ===========================================  =============
Steps                                                                     Expected Result                              Actual Result
========================================================================  ===========================================  =============
empty
========================================================================  ===========================================  =============
