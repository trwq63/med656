"""
These tests are to make sure the username functions correctly
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_username_cannot_be_copied(logoff):
    """
    **Requirements:**

    - 3.1.1.2: Usernames must be unique
    - 3.1.1.3: The system shall assign unique ids to each created patient.
    - 3.1.1.2.1: Users cannot change username to an already existing username

    **Pre Conditions:**

    - logoff fixture

    **Input:**

    - user = 'copy'
    - pwd = 'P@ssword10'
    - email = 'copy@copy.com'
    - first_name = 'Fry'
    - last_name = 'Phillip'
    - address = '304 Wherever Street, New New York City, New New York'
    - phone_number = '123-456-7890'
    - user_2 = 'copy1'
    - email_2 = 'copy2@copy.com'

    ==============================  ====================  =============
    Steps                           Expected Result       Actual Result
    ==============================  ====================  =============
    Request physician account       No errors
    Request same physician account  Username taken error
    Request different account       No errors
    ==============================  ====================  =============
    """
    print('Starting')

    user = 'copy'
    pwd = 'P@ssword10'
    email = 'copy@copy.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'
    user_2 = 'copy1'
    email_2 = 'copy2@copy.com'

    # first use of user copy
    print('Requesting account for user ', user,
          'pwd: ', pwd,
          'email: ', email,
          'first_name: ', first_name,
          'last_name: ', last_name,
          'address: ', address,
          'phone_number: ', phone_number)
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert web_sess.check_request_account()

    # second use of user copy
    print('Requesting account for user ', user,
      'pwd: ', pwd,
      'email: ', email,
      'first_name: ', first_name,
      'last_name: ', last_name,
      'address: ', address,
      'phone_number: ', phone_number)
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert 'Name {} is already taken.'.format(user) in web_sess.get_page()

    # first use of user copy1
    print('Requesting account for user ', user_2,
          'pwd: ', pwd,
          'email: ', email_2,
          'first_name: ', first_name,
          'last_name: ', last_name,
          'address: ', address,
          'phone_number: ', phone_number)
    web_sess.request_account('Physician', user_2, pwd, email_2, first_name, last_name, address, phone_number)
    assert web_sess.check_request_account()
