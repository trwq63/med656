from WebUI.WebUI import WebUI
import pymysql

#db = pymysql.connect()
web_sess = WebUI()


def test_single_file_no_activity(login_tpatient):
    activity_dict = {}
    f = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv'

    assert web_sess.upload_files(f, activity_dict)
    assert 'upload success' in web_sess.get_page()


def test_multi_file_no_activity(login_tpatient):
    activity_dict = {}
    f = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv; ./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_BB.csv'

    assert web_sess.upload_files(f,activity_dict)
    assert 'upload success' in web_sess.get_page()


def test_single_file_one_activity():
    assert False

def test_multi_file_one_activity():
    assert False

def test_single_file_multi_activity():
    assert False

def test_multi_file_multi_activity():
    assert False

def test_zephyr_data_upload():
    assert False

def test_basis_peak_data_upload():
    assert False

def test_mband_data_upload():
    assert False
