import pytest
from WebUI.WebUI import WebUI


@pytest.fixture(scope='session', autouse=True)
def pre_existing_users():
    web_sess = WebUI()
    web_sess.create_patient('testPatient', 'P@ssword10', 'tpatient@aol.com',
                            'test', 'test', 'home', '123-456-7890')
    web_sess.create_patient('testPatient2', 'P@ssword10', 'tpatient@aol.com',
                            'test', 'test', 'home', '123-456-7890')
    web_sess.request_account('Physician', 'testPhysician', 'P@ssword10', 'tphysician@aol.com',
                             'test', 'test', 'home', '123-456-7890')
    web_sess.request_account('Exp Admin', 'testExpAdmin', 'P@ssword10', 'texpadmin@aol.com',
                             'test', 'test', 'home', '123-456-7890')


@pytest.fixture
def logoff():
    web_sess = WebUI()
    if web_sess.check_login():
        web_sess.logoff()
