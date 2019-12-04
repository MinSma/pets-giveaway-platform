import React, { useEffect } from 'react';
import { useParams } from 'react-router';

const PetPage: React.FC = () => {
    const { studyId } = useParams();

    useEffect(() => {
        console.warn(studyId);
    }, [studyId]);

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
