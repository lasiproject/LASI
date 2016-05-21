import React from 'react';

import {
    Row,
    Col,
} from 'react-bootstrap';

import DocumentViewer from './document-viewer/document-viewer';

//import EchartsWordCloudChart from '../echarts/components/WordCloudChart';
//import ThreejsHelloWorld from '../threejs/webgl-buffergeometry-drawcalls/components/HelloWorld';
declare var SystemJS;

export default class Home extends React.Component<any, any> {

    static propTypes = {};

    static defaultProps = {};

    constructor(props) {
        super(props);
        this.state = { data: {} };
        this.componentDidMount();
    }

    componentDidMount() {
        return SystemJS.import('/doc.json!').then(x => {
            this.setState({
                data: x as models.DocumentModel

            });
            this.props = this.state.data;
        });
    }

    render() {
        var data = this.props.data;
        return (
            <div>
                <h1>ES6 JavaScript with Modules Using System.js</h1>
                <Row>
                    <Col xs={12} md={6}>

                    </Col>
                    <Col xs={12} md={6}>

                    </Col>
                </Row>
                <Row>
                    <Col md={12}>
                        <DocumentViewer
                            id={data.id}
                            paragraphs={data.paragraphs}
                            title={data.title}
                            percentComplete={data.percentComplete}
                            progress={data.progress}>
                        </DocumentViewer>
                    </Col>
                </Row>
            </div>
        );
    }
}