import pytest
from WebUI.WebUI import WebUI
from shutil import copy

@pytest.fixture(scope='session',autouse=True)
def pre_existing_users():
    web_sess = WebUI()
    web_sess.create_patient('testPatient','P@ssword10','tpatient@aol.com',\
                            'test','test','home','123-456-7890')
    web_sess.request_account('Physician','testPhysician','P@ssword10','tphysician@aol.com',\
                            'test','test','home','123-456-7890')
    web_sess.request_account('Exp Admin','testExpAdmin','P@ssword10','texpadmin@aol.com',\
                            'test','test','home','123-456-7890')


# make this into a seperate script
@pytest.fixture(scope='session',autouse=True)
def reset_database():
    copy('./Data/database/aspnet-UAHFitVault-20160110104509.mdf',
            '../Source/UAHFitVault/UAHFitVault/App_Data/aspnet-UAHFitVault-20160110104509.mdf')
    copy('./Data/database/aspnet-UAHFitVault-20160110104509_log.ldf',
            '../Source/UAHFitVault/UAHFitVault/App_Data/aspnet-UAHFitVault-20160110104509_log.ldf')