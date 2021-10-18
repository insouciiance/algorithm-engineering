import React from 'react';
import Line from '../../components/Line/Line';

import classes from './LinesSection.scss';

export default function LineSection(props) {
    const { data, name, shouldColorize, accentId } = props;

    let lines = [];

    if (shouldColorize) {
        for (let i = 0; i < data.length; i++) {
            lines.push(
                <Line
                    id={i}
                    key={i}
                    disabled={!shouldColorize(data[i])}
                    isHighlighted={accentId === i}>
                    {data[i]}
                </Line>,
            );
        }
    } else {
        lines = data.map((line, id) => (
            <Line id={id} key={id} isHighlighted={accentId === id}>
                {line}
            </Line>
        ));
    }
    console.log(lines);
    return (
        <div className={classes.LinesSectionWrapper}>
            <p>{name}</p>
            <div className={classes.IndexesList}>{lines}</div>
        </div>
    );
}
