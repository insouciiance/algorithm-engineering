import React from 'react';
import Command from './Command/Command';

import classes from './CommandsList.scss';

export default function CommandsList(props) {
    const { onUpdate } = props;

    return (
        <div className={classes.CommandsListWrapper}>
            <Command name="Add" type="text" method="post" onUpdate={onUpdate} />
            <Command
                name="Remove"
                type="number"
                method="delete"
                onUpdate={onUpdate}
            />
        </div>
    );
}
