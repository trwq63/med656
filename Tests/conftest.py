import pytest
from WebUI.WebUI import WebUI


@pytest.fixture(scope='session', autouse=True)
def pre_existing_users():
    web_sess = WebUI()
    web_sess.request_account('Physician', 'testPhysician', 'P@ssword10', 'tphysician@aol.com',
                             'test', 'test', 'home', '123-456-7890')
    web_sess.request_account('Exp Admin', 'testExpAdmin', 'P@ssword10', 'texpadmin@aol.com',
                             'test', 'test', 'home', '123-456-7890')
    web_sess.create_patient('testPatient', 'P@ssword10', 'tpatient@aol.com',
                            'test', 'test', 'home', '123-456-7890')
    web_sess.create_patient('testPatient2', 'P@ssword10', 'tpatient@aol.com',
                            'test', 'test', 'home', '123-456-7890')


@pytest.fixture
def logoff():
    web_sess = WebUI()
    if web_sess.check_login():
        web_sess.logoff()


@pytest.fixture
def login_tpatient():
    web_sess = WebUI()
    if web_sess.check_login():
        web_sess.logoff()
    web_sess.login('testPatient', 'P@ssword10')
    web_sess.go_home()


@pytest.fixture
def login_tphysician():
    web_sess = WebUI()
    if web_sess.check_login():
        web_sess.logoff()
    web_sess.login('testPhysician', 'P@ssword10')
    web_sess.go_home()


@pytest.fixture
def login_texpadmin():
    web_sess = WebUI()
    if web_sess.check_login():
        web_sess.logoff()
    web_sess.login('testExpAdmin', 'P@ssword10')
    web_sess.go_home()


@pytest.fixture
def login_sysadmin():
    web_sess = WebUI()
    if web_sess.check_login():
        web_sess.logoff()
    web_sess.login('fitadmin', 'Password1!')
    web_sess.go_home()
