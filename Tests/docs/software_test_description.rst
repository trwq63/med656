.. _software_test_description:

============================================
UAHealth Bit Vault Software Test Description
============================================

Contents:

.. toctree::
   :maxdepth: 2

   modules

Scope
-----

Identification
**************

This document shall apply to the final release of the UAHealth Bit Vault Software.

System Overview
***************

The UAHealth Bit Vault Software is designed to be a place for doctors, patients, and administrators to store patient’s
biometric data and run experiments on that data. This is the first deployment of this system.
The software will be deployed on a server in the UAH lab for Dr. Malenkovich. At the end of the development period,
the software will be deployed and ownership shall be transferred to Dr. Malenkovich.

Document Overiew
****************

This document is to describe the testing that will be performed on the UAHealth Bit Vault Software for customer acceptance.
Test descriptions and all dependent information are included in this document.

Referenced Documents
--------------------

None.

Test Preperations
-----------------

Pytest Tests
************

Hardware Preperations
#####################

In order to run the Pytest tests, you must have a physical computer capable of running the latest distribution of Ubuntu.
Follow the hardware requirements for this distribution. Ubuntu is not required to run the tests, but the hardware
requirements for Ubuntu serve as a minimum requirement to run the Pytest tests.

The computer you use must have access to the UAHealt server in order to run the Pytest tests.
The computer can be the same machine as the UAHealth server, but this is not required.

Software Preperations
#####################

You must have Python 3.5.1 installed on this computer. The OS may be Windows 7/2012 or later, or Ubuntu 14.04.4 LTS or
later LTS version. You must also install Pytest and Selenium through “pip”.

Other Preperations
##################

None.

Test Descriptions
-----------------

Pytest test descriptions can bee found here: :ref:'modules'