import pytest
from WebUI.WebUI import WebUI


@pytest.fixture(scope='session', autouse=True)
def pre_existing_users():
    web_sess = WebUI()

    # delete all accounts
    web_sess.delete_patient('testPhysician', 'P@ssword10', 'testPatient')
    web_sess.delete_patient('testPhysician', 'P@ssword10', 'testPatient2')
    web_sess.logoff()

    web_sess.delete_all_accounts()
    web_sess.logoff()

    # add all the needed accounts
    web_sess.request_account('Physician', 'testPhysician', 'P@ssword10', 'tphysician@aol.com',
                             'test', 'physician', 'home', '123-456-7890')
    web_sess.approve_account('test physician')
    web_sess.request_account('Physician', 'testPhysician2', 'P@ssword10', 'tphysician2@aol.com',
                             'test', 'physician2', 'home', '123-456-7890')
    web_sess.approve_account('test physician2')
    web_sess.request_account('Exp Admin', 'testExpAdmin', 'P@ssword10', 'texpadmin@aol.com',
                             'test', 'expadmin', 'home', '123-456-7890')
    web_sess.approve_account('test expadmin')
    web_sess.logoff()

    web_sess.create_patient('testPhysician',
                            'P@ssword10',
                            'testPatient',
                            'P@ssword10',
                            '3 March, 1954',
                            'Alabama',
                            '200',
                            '72',
                            'male',
                            'white',
                            'non_hispanic')
    web_sess.create_patient('testPhysician',
                            'P@ssword10',
                            'testPatient2',
                            'P@ssword10',
                            '10 November, 1999',
                            'Guam',
                            '156',
                            '65',
                            'female',
                            'hawaiian',
                            'non_hispanic')


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
