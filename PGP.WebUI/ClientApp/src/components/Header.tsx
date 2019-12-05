import { faBars } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';
import { useHistory } from 'react-router';
import { routes } from '../utils/routes';

const Header: React.FC = () => {
    const [collapseToggle, setCollapseToggle] = useState<boolean>(true);
    const history = useHistory();
    const authToken = localStorage.getItem('jwtToken');

    return (
        <header className={`topnav ${collapseToggle ? '' : 'responsive'}`}>
            <a className="cursor-pointer" onClick={() => history.push(routes.HOME())}>
                Home
            </a>
            {authToken ? (
                <a
                    className="float-right cursor-pointer"
                    onClick={() => {
                        localStorage.removeItem('jwtToken');
                        history.push('/login');
                    }}
                >
                    Logout
                </a>
            ) : (
                <>
                    <a className="float-right cursor-pointer" onClick={() => history.push(routes.LOGIN_PAGE())}>
                        Login
                    </a>
                    <a className="float-right cursor-pointer" onClick={() => history.push(routes.REGISTER_PAGE())}>
                        Register
                    </a>
                </>
            )}
            <a className="icon" onClick={() => setCollapseToggle(!collapseToggle)}>
                <FontAwesomeIcon icon={faBars} />
            </a>
        </header>
    );
};

export default Header;
