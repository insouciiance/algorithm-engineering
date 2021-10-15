import React from 'react';
import Line from '../../components/Line/Line';

import classes from './LinesSection.scss';

export default function DataSection(props) {
    const { data, name } = props;

    return (
        <div className={classes.LinesSectionWrapper}>
            <p>{name}</p>
            <div className={classes.IndexesList}>
                {data.map((line, id) => (
                    <Line id={id} key={id}>
                        {line}
                    </Line>
                ))}
            </div>
        </div>
    );
}
