import React from 'react';
import Command from './Command/Command';

import classes from './CommandsList.scss';

export default function CommandsList(props) {
    const { onPostUpdate, onDeleteUpdate, onGetUpdate } = props;

    return (
        <div className={classes.CommandsListWrapper}>
            <Command
                name="Add"
                type="text"
                method="post"
                onUpdate={onPostUpdate}
            />
            <Command
                name="Remove"
                type="number"
                method="delete"
                onUpdate={onDeleteUpdate}
            />
            <Command
                name="Find"
                type="number"
                method="get"
                onUpdate={onGetUpdate}
            />
        </div>
    );
}
