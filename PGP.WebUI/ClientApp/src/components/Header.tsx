import { faBars } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';
import { useHistory } from 'react-router';
import { deleteToken, getTokenDecoded } from '../apiClient';
import { routes } from '../utils/routes';

const Header: React.FC = () => {
    const [collapseToggle, setCollapseToggle] = useState<boolean>(true);
    const history = useHistory();
    const decodedToken = getTokenDecoded();

    return (
        <header className={`topnav ${collapseToggle ? '' : 'responsive'}`}>
            <a className="cursor-pointer" onClick={() => history.push(routes.HOME())}>
                Home
            </a>
            {decodedToken ? (
                <>
                    <a className="cursor-pointer" onClick={() => history.push(routes.LIKED_PETS_PAGE())}>
                        Liked pets
                    </a>
                    {decodedToken.role === 'Admin' && (
                        <>
                            <a className="cursor-pointer" onClick={() => history.push(routes.USERS_PAGE())}>
                                Users
                            </a>
                            <a className="cursor-pointer" onClick={() => history.push(routes.COMMENTS_PAGE())}>
                                Comments
                            </a>
                            <a className="cursor-pointer" onClick={() => history.push(routes.CATEGORIES_PAGE())}>
                                Categories
                            </a>
                        </>
                    )}
                    {(decodedToken.role === 'Moderator' || decodedToken.role === 'Admin') && (
                        <a className="cursor-pointer" onClick={() => history.push(routes.PETS_PAGE())}>
                            Pets
                        </a>
                    )}
                    <a
                        className="float-right cursor-pointer"
                        onClick={() => {
                            deleteToken();
                            history.push('/login');
                        }}
                    >
                        Logout
                    </a>
                </>
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
