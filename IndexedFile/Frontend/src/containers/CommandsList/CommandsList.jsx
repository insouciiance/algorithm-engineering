import React from 'react';
import Command from './Command/Command';

import classes from './CommandsList.scss';

export default function CommandsList(props) {
    const { onAdd, onDelete, onDeleteAll, onFind } = props;

    return (
        <div className={classes.CommandsListWrapper}>
            <Command
                name="Add"
                type="text"
                onUpdate={onAdd}
                onlyButton={false}
            />
            <Command
                name="Remove"
                type="number"
                onUpdate={onDelete}
                onlyButton={false}
            />
            <Command
                name="Find"
                type="number"
                onUpdate={onFind}
                onlyButton={false}
            />
            <Command
                name="Remove all"
                onUpdate={onDeleteAll}
                onlyButton={true}
            />
        </div>
    );
}
