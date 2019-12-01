import React from 'react';
import Footer from './Footer';
import Header from './Header';

const Layout: React.FC = props => {
    return (
        <div className="wrapper">
            <Header />
            <main>{props.children}</main>
            <Footer />
        </div>
    );
};

export default Layout;
