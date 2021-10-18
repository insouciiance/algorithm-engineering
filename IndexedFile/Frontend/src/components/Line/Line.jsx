import React from 'react';

import classes from './Line.scss';

export default function Line(props) {
    const { id, children, disabled, isHighlighted } = props;

    return (
        <div
            className={classes.LineWrapper}
            disabled={disabled}
            highlighted={isHighlighted.toString()}>
            <span className={classes.Id}>{id}</span>
            <span className={classes.Value}>{children}</span>
        </div>
    );
}
