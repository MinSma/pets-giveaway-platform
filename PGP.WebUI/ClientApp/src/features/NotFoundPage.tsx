import { Button } from 'evergreen-ui';
import React from 'react';
import { useHistory } from 'react-router';

const NotFoundPage: React.FC = () => {
    const history = useHistory();

    return (
        <div className="container mt-5 mb-5 text-center">
            <h1>401</h1>
            <h2>Page not found</h2>
            <h4>Sorry, we couldn't find the page you were looking for. We suggest that you return to main sections.</h4>
            <Button onClick={() => history.push('/')}>Go to the main page</Button>
        </div>
    );
};

export default NotFoundPage;
