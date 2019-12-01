import React from 'react';

const Layout: React.FC = props => {
    return (
        <div className="wrapper">
            <header>Header</header>
            <main>{props.children}</main>
            <footer>Footer</footer>
        </div>
    );
};

export default Layout;
