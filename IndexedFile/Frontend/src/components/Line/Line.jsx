import React from 'react';

import classes from './Line.scss';

export default function Line(props) {
    const { id, children, disabled } = props;

    return (
        <div className={classes.LineWrapper} disabled={disabled}>
            <span className={classes.Id}>{id}</span>
            <span className={classes.Value}>{children}</span>
        </div>
    );
}
