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
        };
    }

    componentDidMount() {
        axios.get('indexedfile').then(res => {
            this.setState({
                data: res.data.data,
                indexes: res.data.indexes,
            });
        });
    }

    onUpdate = (data, indexes) => {
        this.setState({
            data,
            indexes,
        });
    };

    render() {
        return (
            <div className={classes.MainWrapper}>
                <h2 className={classes.MainGreeting}>Indexed file test</h2>
                <div>
                    <CommandsList onUpdate={this.onUpdate} />
                </div>
                <div className={classes.IndexedFileContainer}>
                    <div className={classes.IndexedSection}>
                        <LinesSection
                            data={this.state.indexes}
                            name="Indexes" />
                    </div>
                    <div className={classes.DataSection}>
                        <LinesSection
                            data={this.state.data}
                            name="Data"
                            shouldColorize={line => line.split(',')[2] === 'true'} />
                    </div>
                </div>
            </div>
        );
    }
}
