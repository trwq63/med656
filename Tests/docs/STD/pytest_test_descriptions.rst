.. _pytest_test_descriptions:

============
Pytest Tests
============

.. contents:: Table of Contents

Test Preparations
-----------------

Hardware Preparations
#####################

In order to run the Pytest tests, you must have a physical computer capable of running the latest distribution of Ubuntu.
Follow the hardware requirements for this distribution. Ubuntu is not required to run the tests, but the hardware
requirements for Ubuntu serve as a minimum requirement to run the Pytest tests.

The computer you use must have access to the UAHealth server in order to run the Pytest tests.
The computer can be the same machine as the UAHealth server, but this is not required.

Software Preparations
#####################

You must have the latest Active Python 3.4 installed on this computer. It can be found Here_. The OS may be Windows
7/2012 or later. The required python packages can be installed by running::

   pip install -r ./Tests/requirements.txt

.. _Here: http://www.activestate.com/activepython/downloads

Other Preparations
##################

Because patient accounts are not deleted from the database, you need to manually remove the following patient accounts
before performing tesitng:

- testPatient
- testPatient2
- pfry

Test Execution
--------------

To run these tests, open a command prompt and navigate to the Tests folder (<project_path>/med656/Tests). Then execute
the following command:

py.test -v -s test_create_account.py test_edit_account.py test_experiment.py test_login.py test_password.py test_security.py test_upload.py test_username.py test_export.py

The test report will be printed out to the screen. It will display all the tests that were run along with their pass/fail
status.

Test Details
------------

Follow the table below to find details of the test automation.

.. toctree::
   :maxdepth: 4

   test_create_account
   test_edit_account
   test_login
   test_password
   test_security
   test_upload
   test_username
   conftest
   WebUI
   
