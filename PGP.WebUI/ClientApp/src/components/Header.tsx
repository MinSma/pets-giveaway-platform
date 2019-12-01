import { faBars } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';

const Header: React.FC = () => {
    const [collapseToggle, setCollapseToggle] = useState<boolean>(true);

    return (
        <header className={`topnav ${collapseToggle ? '' : 'responsive'}`}>
            <a href="#">Home</a>
            <a href="#" className="float-right">
                Login
            </a>
            <a href="#" className="float-right">
                Register
            </a>
            <a href="#" className="icon" onClick={() => setCollapseToggle(!collapseToggle)}>
                <FontAwesomeIcon icon={faBars} />
            </a>
        </header>
    );
};

export default Header;
