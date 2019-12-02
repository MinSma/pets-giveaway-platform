import React, { useEffect } from 'react';
import useReactRouter from 'use-react-router';

const PetPage: React.FC = () => {
    const { history } = useReactRouter();

    useEffect(() => {
        console.warn(history);
    }, [history]);

    return (
        <div className="container">
            <div className="row">
                <div className="col-lg-6 col-md-12 col-xs-12"></div>
                <div className="col-lg-6 col-md-12 col-xs-12"></div>
            </div>
        </div>
    );
};

export default PetPage;
