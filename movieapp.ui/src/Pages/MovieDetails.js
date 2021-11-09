import React from "react";
import { withRouter } from 'react-router';
import { Link } from 'react-router-dom';

class MovieDetails extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            movieDetails: [],
            loading: true,
        }
    }

    async componentDidMount(){
        try {
            const id = this.props.match.params.id;
            const response = await fetch(`https://localhost:5001/api/movies/movie/${id}`);
            const data = await response.json();
            this.setState({movieDetails: data, loading: false});

            document.title = `${this.state.movieDetails.title}`;

        } catch (e) {
            console.error(e);
        }
    }

    render() {
        if(this.state.loading)
            return (
                <div className="container text-center pt-5">
                    <div className="spinner-border text-primary" role="status">
                        <span className="sr-only"></span>
                    </div>
                </div>
            )
        return(
            <div className="container">
                <div className="row">
                    <div className="col"></div>
                    <div className="col-8 p-5">
                        <div className="row m-2">
                            <div className="col">
                                <Link to="/">
                                <button type="button" className="btn btn-primary"><i class="bi bi-arrow-left"></i>Back</button>
                                </Link>
                            </div>
                        </div>
                        <div className="row m-2">
                            <label for="title" className="col col-form-label">Title</label>
                            <div className="col">
                                <input type="text" className="form-control" id="title" value={this.state.movieDetails.title} readOnly></input>
                            </div>
                        </div>
                        <div className="row m-2">
                            <label for="genre" className="col col-form-label">Genre</label>
                            <div className="col">
                                <input type="text" className="form-control" id="genre" value={this.state.movieDetails.genre} readOnly></input>
                            </div>
                        </div>
                        <div className="row m-2">
                            <label for="releaseDate" className="col col-form-label">Release Date</label>
                            <div className="col">
                                <input type="text" className="form-control" id="releaseDate" value={this.state.movieDetails.releaseDate} readOnly></input>
                            </div>
                        </div>
                        <div className="row m-2">
                            <label for="actors" className="col col-form-label">Actors</label>
                            <div className="col">
                                <input type="text" className="form-control" id="actors" value={this.state.movieDetails.actors} readOnly></input>
                            </div>
                        </div>
                        <div className="row m-2">
                            <label for="about" className="col col-form-label">About</label>
                            <div className="col">
                                <textarea type="text" className="form-control" id="about" value={this.state.movieDetails.about} rows="5" readOnly></textarea>
                            </div>
                        </div>
                    </div>
                    <div className="col"></div>
                </div>
            </div>
        )
    }
}

export default withRouter(MovieDetails);