from WebUI.WebUI import WebUI

web_sess = WebUI()

def test_patient_to_patient():
    web_sess.login('testPatient','P@ssword10')
    ps = web_sess.driver.get('')
    assert 'Restricted' in ps

def test_patient_to_physician():
    web_sess.login()