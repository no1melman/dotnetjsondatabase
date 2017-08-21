import React from 'react';

const Spinner = () => {
    return (
        <div>
            <i className="fa fa-circle-o-notch fa-spin fa-3x fa-fw" />
            <span className="sr-only">Loading...</span>
        </div>
    );
};

export default Spinner;