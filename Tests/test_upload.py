from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_single_file_no_activity(login_tpatient):
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files
    - 3.2.4.2: System shall support Basis Peak data

    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activity_dict = {}
    - file = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv'


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    activity_dict = {}
    file = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv'

    assert web_sess.upload_files(file, activity_dict)
    assert 'upload success' in web_sess.get_page()


def test_multi_file_no_activity(login_tpatient):
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files


    **Pre Conditions:**

    - login_tpatient fixture

    **Input:**

    - activity_dict = {}
    - files = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv; ./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_BB.csv'


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    activity_dict = {}
    files = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv; ./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_BB.csv'

    assert web_sess.upload_files(files,activity_dict)
    assert 'upload success' in web_sess.get_page()


def test_single_file_one_activity():
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.2: System shall provide ability to assign activity
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files


    **Pre Conditions:**


    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    assert False

def test_multi_file_one_activity():
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.2: System shall provide ability to assign activity
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files


    **Pre Conditions:**


    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    assert False

def test_single_file_multi_activity():
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.2: System shall provide ability to assign activity
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files


    **Pre Conditions:**


    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    assert False

def test_multi_file_multi_activity():
    """
    **Requirements:**

    - 3.1.2: System shall process medical data
    - 3.1.2.1: System shall process one or more medical data file at a time
    - 3.1.2.2: System shall provide ability to assign activity
    - 3.1.2.3: There will be an interface to select data and activities
    - 3.1.2.5: System shall process CSV files


    **Pre Conditions:**


    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    assert False

def test_zephyr_data_upload():
    """
    **Requirements:**

    - 3.2.4.1: System shall support Zephyr data
    - 3.1.2.6: System will process raw data files

    **Pre Conditions:**


    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    assert False

def test_mband_data_upload():
    """
    **Requirements:**

    - 3.2.4.3: System shall support Microsoft Band data


    **Pre Conditions:**


    **Input:**


    ===============  =================  =============
    Steps            Expected Result    Actual Result
    ===============  =================  =============
    empty
    ===============  =================  =============
    """
    assert False
