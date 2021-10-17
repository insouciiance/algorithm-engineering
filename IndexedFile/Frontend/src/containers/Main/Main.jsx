import React, { Component } from 'react';
import LinesSection from '../LinesSection/LinesSection';
import CommandsList from '../CommandsList/CommandsList';

import axios from '../../axiosInstance';
import classes from './Main.scss';

export default class Main extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: [],
            indexes: [],
            accentIndexId: -1,
            accentDataId: -1
        };
    }

    componentDidMount() {
        axios.get('indexedfile').then(res => {
            this.setState({
                data: res.data.data,
                indexes: res.data.indexes,
                accentIndexId: -1,
                accentDataId: -1
            });
        });
    }

    onGetPostUpdate = (data, indexes) => {
        this.setState({
            data,
            indexes,
            accentIndexId: -1,
            accentDataId: -1
        });
    };

    onGetUpdate = lineId => {
        const { indexes } = this.state;
        const dataIndex = +indexes[lineId].split(',')[1];

        this.setState({
            accentIndexId: lineId,
            accentDataId: dataIndex
        });
    }

    render() {
        const { indexes, data, accentIndexId, accentDataId } = this.state;

        return (
            <div className={classes.MainWrapper}>
                <h2 className={classes.MainGreeting}>Indexed file test</h2>
                <CommandsList
                    onPostUpdate={this.onGetPostUpdate}
                    onDeleteUpdate={this.onGetPostUpdate}
                    onGetUpdate={this.onGetUpdate} />
                <div className={classes.IndexedFileContainer}>
                    <div className={classes.IndexedSection}>
                        <LinesSection
                            data={indexes}
                            name="Indexes"
                            accentId={accentIndexId} />
                    </div>
                    <div className={classes.DataSection}>
                        <LinesSection
                            data={data}
                            name="Data"
                            shouldColorize={line => line.split(',')[2] === 'true'}
                            accentId={accentDataId}/>
                    </div>
                </div>
            </div>
        );
    }
}
