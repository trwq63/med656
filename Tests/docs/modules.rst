.. _modules:

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

You must have Python 3.5.1 installed on this computer. The OS may be Windows 7/2012 or later, or Ubuntu 14.04.4 LTS or
later LTS version. You must also install Pytest and Selenium through “pip”.

Other Preparations
##################

None.

Test Details
------------

.. toctree::
   :maxdepth: 4

   WebUI
   conftest
   setup_db
   test_create_account
   test_edit_account
   test_login
   test_password
   test_security
   test_upload
   test_username