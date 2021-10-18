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
            accentDataId: -1,
            comparisonsCount: 0,
        };
    }

    componentDidMount() {
        axios.get('indexedfile').then(res => {
            this.setState({
                data: res.data.data,
                indexes: res.data.indexes,
                accentIndexId: -1,
                accentDataId: -1,
                comparisonsCount: 0,
            });
        });
    }

    onAdd = value => {
        axios.post('indexedfile', `value=${value}`).then(res =>
            this.setState({
                data: res.data.data,
                indexes: res.data.indexes,
                accentIndexId: -1,
                accentDataId: -1,
                comparisonsCount: 0,
            }),
        );
    };

    onDelete = value => {
        axios.delete(`indexedfile/${value}`).then(res =>
            this.setState({
                data: res.data.data,
                indexes: res.data.indexes,
                accentIndexId: -1,
                accentDataId: -1,
                comparisonsCount: 0,
            }),
        );
    };

    onDeleteAll = () => {
        axios.delete(`indexedfile/`).then(res =>
            this.setState({
                data: res.data.data,
                indexes: res.data.indexes,
                accentIndexId: -1,
                accentDataId: -1,
                comparisonsCount: 0,
            }),
        );
    };

    onFind = id => {
        axios.get(`indexedfile/${id}`).then(res => {
            const { indexes } = this.state;
            const { lineId, comparisonsCount } = res.data;

            if (lineId > -1) {
                const dataIndex = +indexes[lineId].split(',')[1];

                this.setState({
                    accentIndexId: lineId,
                    accentDataId: dataIndex,
                    comparisonsCount,
                });
            } else {
                this.setState({
                    comparisonsCount,
                });
            }
        });
    };

    render() {
        const { indexes, data, accentIndexId, accentDataId, comparisonsCount } =
            this.state;

        return (
            <div className={classes.MainWrapper}>
                <h2 className={classes.MainGreeting}>Indexed file test</h2>
                <CommandsList
                    onAdd={this.onAdd}
                    onDelete={this.onDelete}
                    onDeleteAll={this.onDeleteAll}
                    onFind={this.onFind}
                />
                <div className={classes.ComparisonsCountContainer}>
                    {comparisonsCount ? (
                        <h4>Comparisons count: {comparisonsCount}</h4>
                    ) : null}
                </div>
                <div className={classes.IndexedFileContainer}>
                    <div className={classes.IndexedSection}>
                        <LinesSection
                            data={indexes}
                            name="Indexes"
                            accentId={accentIndexId}
                        />
                    </div>
                    <div className={classes.DataSection}>
                        <LinesSection
                            data={data}
                            name="Data"
                            shouldColorize={line =>
                                line.split(',')[2] === 'true'
                            }
                            accentId={accentDataId}
                        />
                    </div>
                </div>
            </div>
        );
    }
}
