from WebUI.WebUI import WebUI
import pymysql

#db = pymysql.connect()
web_sess = WebUI()
web_sess.login("testPatient","P@ssword10")

def test_single_file_no_activity():
    activity_dict = {}
    f = ''

    #ps = web_sess.upload_file(f,activity_dict)
    #entry = db.query('')

    assert False

def test_multi_file_no_activity():
    assert False

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
