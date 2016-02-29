from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_patient_to_patient():
    """
    This procedure tests one patient trying to view a different patients data

    :return:
    """
    web_sess.login('testPatient', 'P@ssword10')
    ps = web_sess.driver.get('')
    assert 'Restricted' in ps


def test_physician_to_patient():
    """
    This procedure tests a physician cannot view a patient's data that belongs to a patient
    who belongs to a different physician.

    :return:
    """
    web_sess.login('testPatient', 'P@ssword10')
    ps = web_sess.driver.get('')
    assert 'Restricted' in ps


def test_patient_cannot_create_patient():
    """
    This procedure tests that a patient cannot create another patient.

    :return:
    """
    assert False


def test_exp_admin_cannot_create_patient():
    """
    This procedure tests that an experiment admin cannot create another patient.
    :return:
    """
    assert False
