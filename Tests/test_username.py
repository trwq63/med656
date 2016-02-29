"""
These tests are to make sure the username functions correctly
"""
from WebUI.WebUI import WebUI

web_sess = WebUI()


def test_username_cannot_be_copied(logoff):
    """
    This test creates a new user, tries to create another user with the same name, then creates third unique user
    Validatin is done on all three steps. This verifies that the username must be unique.
    Note: Need a step added to check that usernames cannot be changed to a copy

    :param logoff: Makes sure the session is in a logged off state before the test starts.
    :return:
    """
    user = 'copy'
    pwd = 'P@ssword10'
    email = 'copy@copy.com'
    first_name = 'Fry'
    last_name = 'Phillip'
    address = '304 Wherever Street, New New York City, New New York'
    phone_number = '123-456-7890'

    # first use of user copy
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert web_sess.check_request_account()

    # second use of user copy
    web_sess.request_account('Physician', user, pwd, email, first_name, last_name, address, phone_number)
    assert 'Name {} is already taken.'.format(user) in web_sess.get_page()

    # first use of user copy1
    web_sess.request_account('Physician', 'copy1', pwd, 'copy1@copy.com', first_name, last_name, address, phone_number)
    assert web_sess.check_request_account()
