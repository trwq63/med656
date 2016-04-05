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
