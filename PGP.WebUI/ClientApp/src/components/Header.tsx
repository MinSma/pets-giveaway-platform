//import { faBars } from '@fortawesome/free-solid-svg-icons';
//import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';
import useReactRouter from 'use-react-router';
import { routes } from '../utils/routes';

const Header: React.FC = () => {
    const [collapseToggle, setCollapseToggle] = useState<boolean>(true);
    const { history } = useReactRouter();

    return (
        <header className={`topnav ${collapseToggle ? '' : 'responsive'}`}>
            <a className="cursor-pointer" onClick={() => history.push(routes.HOME())}>
                Home
            </a>
            <a className="float-right cursor-pointer" onClick={() => history.push(routes.LOGIN_PAGE())}>
                Login
            </a>
            <a className="float-right cursor-pointer" onClick={() => history.push(routes.REGISTER_PAGE())}>
                Register
            </a>
            <a href="#" className="icon" onClick={() => setCollapseToggle(!collapseToggle)}>
                {/* <FontAwesomeIcon icon={faBars} /> */}
            </a>
        </header>
    );
};

export default Header;
