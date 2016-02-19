from WebUI.WebUI import WebUI
import pymysql

#db = pymysql.connect()
web_sess = WebUI()


def test_single_file_no_activity(logoff):
    activity_dict = {}
    f = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv'

    web_sess.login("testPatient","P@ssword10")
    if web_sess.check_login():
        assert web_sess.upload_files(f,activity_dict)
    #entry = db.query('')
    assert 'upload success' in web_sess.get_page()


def test_multi_file_no_activity(logoff):
    activity_dict = {}
    f = './Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_Accel.csv; ./Data/Zephyr/ZephyrTestData/2015_06_24__23_05_14_BB.csv'

    web_sess.login("testPatient","P@ssword10")
    if web_sess.check_login():
        assert web_sess.upload_files(f,activity_dict)
    #entry = db.query('')
    assert 'upload success' in web_sess.get_page()


def test_single_file_one_activity():
    assert False

def test_multi_file_one_activity():
    assert False

def test_single_file_multi_activity():
    assert False

def test_multi_file_multi_activity():
    assert False

def test_zephyr():
    assert False

def test_basis_peak():
    assert False

def test_mband():
    assert False
