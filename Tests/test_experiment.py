"""
These test cases are designed to test the ability to create experiments
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_create_experiment(login_texpadmin):
    """
    **Requirements:**

    - 3.1.4: The system shall allow experiments to be created.
    - 3.1.4.1.1: Experiment administrators shall have the ability to save the experiments that they have created.
    - 3.1.4.3: The system shall provide Experiment Administrators with a query builder used to create experiments.

    **Pre Conditions:**

    - login_texpadmin fixture

    **Input:**

    - expName = 'test experiment'
    - startAge = '19'
    - endAge = '24'
    - startHght = '60'
    - endHght = '72'
    - startWght = '130'
    - endWght = '200'
    - genders = ['male']
    - races = ['white']
    - ethnicities = ['hispanic']
    - locations = ['Alabama']

    =================  ============================  =============
    Steps              Expected Result               Actual Result
    =================  ============================  =============
    create experiemnt  no errors, see success! page
    =================  ============================  =============
    """
    print('Starting')
    expName = 'test experiment'
    startAge = '60'
    endAge = '70'
    startHght = '60'
    endHght = '78'
    startWght = '130'
    endWght = '210'
    genders = ['male']
    races = ['white']
    ethnicities = ['non_hispanic']
    locations = ['Alabama']

    print('Creating experiment')
    assert web_sess.create_experiment(expName, startAge, endAge, startHght, endHght, startWght, endWght, genders, races, ethnicities, locations)
    assert 'Success!' in web_sess.get_page()


def test_view_experiment_exp_admin(login_texpadmin):
    """
    **Requirements:**

    - 3.1.4.2: Experiment results shall be viewable by experiment administrators and physicians.

    **Pre Conditions:**

    - login_texpadmin fixture

    **Input:**

    - name = 'test experiment'

    ===================  ==================================  =============
    Steps                Expected Result                     Actual Result
    ===================  ==================================  =============
    view the experiment  no errors, on view experiment page
    ===================  ==================================  =============
    """
    print('Starting')
    name = 'test experiment'

    print('Navigating to experiment ', name)
    assert web_sess.view_experiment(name)
    print('Checking that we have at least 1 patient in our experiment')
    web_sess.driver.find_element_by_css_selector('input[value="View Data"]')



def test_view_experiment_physician(login_tphysician):
    """
    **Requirements:**

    - 3.1.4.2: Experiment results shall be viewable by experiment administrators and physicians.

    **Pre Conditions:**

    - login_texpadmin fixture

    **Input:**

    - name = 'test experiment'

    ===================  ==================================  =============
    Steps                Expected Result                     Actual Result
    ===================  ==================================  =============
    view the experiment  no errors, on view experiment page
    ===================  ==================================  =============
    """
    print('Starting')
    name = 'test experiment'

    print('Navigating to experiment ', name)
    assert web_sess.view_experiment(name)
    print('Checking that we have at least 1 patient in our experiment')
    web_sess.driver.find_element_by_css_selector('input[value="View Data"]')



def test_delete_experiment(login_texpadmin):
    """
    **Requirements:**

    - 3.1.4.4: The system shall allow Experiment Administrators to delete experiments.

    **Pre Conditions:**

    - login_texpadmin fixture

    **Input:**

    - name = 'test experiment'

    =================  ======================================  =============
    Steps              Expected Result                         Actual Result
    =================  ======================================  =============
    create experiemnt  no errors, on delete confirmation page
    =================  ======================================  =============
    """
    print('Starting')
    name = 'test experiment'

    assert web_sess.delete_experiment(name)
    assert 'Delete Experiment Confirmation' in web_sess.get_page()

