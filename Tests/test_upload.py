from WebUI.WebUI import WebUI
from os import path
import time

web_sess = WebUI()


def test_single_file_no_activity(login_tpatient):
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files

    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activities = []
    - file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv')


    ===============  =======================================  =============
    Steps            Expected Result                          Actual Result
    ===============  =======================================  =============
    upload file      no errors, and returned to /PatientData
    ===============  =======================================  =============
    """
    print('Starting')
    activities = []
    file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Summary.csv')

    # upload the file
    print('Uploading file')
    assert web_sess.upload_files(file, 2015, 'June', 24, 2015, 'June', 24, 'zephyr', activities)
    # check for server error in web page
    print('Checking for server error')
    assert "Server Error in '/' Application" not in web_sess.get_page()
    # check that current url is now .../PatientData
    print('Checking we were sent back to patient data')
    assert '/PatientData' in web_sess.driver.current_url


def test_single_file_one_activity(login_tpatient):
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.2: System shall provide ability to assign activity
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files


    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activities = [{'type':'Home Eating', 'year':2015, 'month':'June', 'day':24, 'startTime': '11:00 PM', 'endTime': '11:30 PM'}]
    - file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv')

    ===============  =======================================  =============
    Steps            Expected Result                          Actual Result
    ===============  =======================================  =============
    upload file      no errors, and returned to /PatientData
    ===============  =======================================  =============
    """
    print('Starting')
    activities = [
        {'type':'Home Eating',
         'year':2015,
         'month':'June',
         'day':24,
         'startTime': '11:00 PM',
         'endTime': '11:30 PM'}
    ]
    file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Summary.csv')

    # upload the file
    print('Uploading file')
    assert web_sess.upload_files(file, 2015, 'June', 24, 2015, 'June', 24, 'zephyr', activities)
    # check for server error in web page
    print('Checking for server error')
    assert "Server Error in '/' Application" not in web_sess.get_page()
    # check that current url is now .../PatientData
    print('Checking we were sent back to patient data')
    assert '/PatientData' in web_sess.driver.current_url


def test_single_file_multi_activity(login_tpatient):
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.2: System shall provide ability to assign activity
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files


    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activities

      - activity 1

        - 'type':'Home Eating',
        - 'year':2015,
        - 'month':'June',
        - 'day':24,
        - 'startTime': '11:00 PM',
        - 'endTime': '11:30 PM'},

      - activity 2

        - 'type':'Home Cooking',
        - 'year':2015,
        - 'month':'June',
        - 'day':24,
        - 'startTime': '12:00 PM',
        - 'endTime': '12:30 PM'

    - file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv')

    ===============  =======================================  =============
    Steps            Expected Result                          Actual Result
    ===============  =======================================  =============
    upload file      no errors, and returned to /PatientData
    ===============  =======================================  =============
    """
    print('Starting')
    activities = [
        {
            'type':'Home Eating',
            'year':2015,
            'month':'June',
            'day':24,
            'startTime': '11:00 PM',
            'endTime': '11:30 PM'},
        {
            'type':'Home Cooking',
            'year':2015,
            'month':'June',
            'day':24,
            'startTime': '12:00 PM',
            'endTime': '12:30 PM'
        }
    ]
    file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Summary.csv')

    # upload the file
    print('Uploading file')
    assert web_sess.upload_files(file, 2015, 'June', 24, 2015, 'June', 24, 'zephyr', activities)
    # check for server error in web page
    print('Checking for server error')
    assert "Server Error in '/' Application" not in web_sess.get_page()
    # check that current url is now .../PatientData
    print('Checking we were sent back to patient data')
    assert '/PatientData' in web_sess.driver.current_url


def test_zephyr_data_upload(login_tpatient):
    """
    **Requirements:**

    - 3.2.4.1: System shall support Zephyr data

    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activities = []
    - file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_BB.dat')

    ===============  =======================================  =============
    Steps            Expected Result                          Actual Result
    ===============  =======================================  =============
    upload file      no errors, and returned to /PatientData
    ===============  =======================================  =============
    """
    print('Starting')
    activities = []
    file = path.abspath('./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Summary.csv')

    # upload the file
    print('Uploading file')
    assert web_sess.upload_files(file, 2015, 'June', 24, 2015, 'June', 24, 'zephyr', activities)
    # check for server error in web page
    print('Checking for server error')
    assert "Server Error in '/' Application" not in web_sess.get_page()
    # check that current url is now .../PatientData
    print('Checking we were sent back to patient data')
    assert '/PatientData' in web_sess.driver.current_url


def test_mband_data_upload(login_tpatient):
    """
    **Requirements:**

    - 3.2.4.3: System shall support Microsoft Band data


    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activities = []
    - file = path.abspath('./Data/MBand/')

    ===============  =======================================  =============
    Steps            Expected Result                          Actual Result
    ===============  =======================================  =============
    upload file      no errors, and returned to /PatientData
    ===============  =======================================  =============
    """
    print('Starting')
    activities = []
    file = path.abspath('./Data/MBand/HeartRate_2015_6_24.csv')

    # upload the file
    print('Uploading file')
    assert web_sess.upload_files(file, 2015, 'June', 24, 2015, 'June', 24, 'band', activities)
    # check for server error in web page
    print('Checking for server error')
    assert "Server Error in '/' Application" not in web_sess.get_page()
    # check that current url is now .../PatientData
    print('Checking we were sent back to patient data')
    assert '/PatientData' in web_sess.driver.current_url


def test_basis_data_upload(login_tpatient):
    """
    **Requirements:**

    - 3.2.4.2: System shall support Basis Peak data


    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activities = []
    - file = path.abspath('./Data/BasisPeak/bodymetrics_2012-01-30T00-00-00_2016-01-18T04-51-00.csv')

    ===============  =======================================  =============
    Steps            Expected Result                          Actual Result
    ===============  =======================================  =============
    upload file      no errors, and returned to /PatientData
    ===============  =======================================  =============
    """
    print('Starting')
    activities = []
    file = path.abspath('./Data/BasisPeak/bodymetrics_2012-01-30T00-00-00_2016-01-18T04-51-00.csv')

    # upload the file
    print('Uploading file')
    assert web_sess.upload_files(file, 2012, 'January', 30, 2012, 'January', 30, 'basis', activities)
    time.sleep(10)
    # check for server error in web page
    print('Checking for server error')
    assert "Server Error in '/' Application" not in web_sess.get_page()
    # check that current url is now .../PatientData
    print('Checking we were sent back to patient data')
    assert '/PatientData' in web_sess.driver.current_url
