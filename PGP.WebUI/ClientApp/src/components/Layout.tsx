import React from 'react';
import Header from './Header';

const Layout: React.FC = props => {
    return (
        <div className="wrapper">
            <Header />
            <main>{props.children}</main>
            <footer>Footer</footer>
        </div>
    );
};

export default Layout;
