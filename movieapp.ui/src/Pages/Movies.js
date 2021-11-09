import React from "react";
import { Link } from 'react-router-dom';

class Movies extends React.Component {
    constructor(props) {
        super(props);
        document.title = "Movies";

        this.state = {
            movieData: [],
            loading: true,
            results: [],
            timeout: null,
            search: null,
        }
        this.sortByAscending = this.sortByAscending.bind(this);
        this.sortByDescending = this.sortByDescending.bind(this);
    }

    async componentDidMount(){
        try {
            const response = await fetch("https://localhost:5001/api/movies");
            const data = await response.json();
            this.setState({movieData: data, results: data, loading: false});
        } catch (e) {
            console.error(e);
        }
    }

    sortByAscending() {
        const titles = this.state.results;
        
        titles.sort((a, b) => a.title.localeCompare(b.title));
        
        this.setState({
          results: titles
        });
      }
    
      sortByDescending() {
        const titles = this.state.results;
        
        titles.sort((a, b) => b.title.localeCompare(a.title));
        
        this.setState({
          results: titles
        });
      }

      async handleSearch(event) {
        let value = event.target.value.toLowerCase();
        let titles = this.state.movieData;


        var result = titles.filter((str)=>{
            return str.title.toLowerCase().indexOf(value.toLowerCase()) >= 0; 
        });

        if(result.length === 0){
            this.noTitles(value, result);

        } else{
            this.setState({
                results: result
            });
        }
      }

      async noTitles(value, results) {
            clearTimeout(this.state.timeout);

            const timeout = setTimeout(function() {
                console.log("No titles found");
                console.log("Search: " + value);
            }, 1000);

            this.setState({ timeout:  timeout , results: results, search: value });
      }
      
      async search(){
        if(this.state.search !== null){
            try {
                this.setState({loading: true});
                const response = await fetch(`https://localhost:5001/api/movies/${this.state.search.toString()}`);
                const data = await response.json();
                this.setState({movieData: data, results: data, loading: false, search: null});
            } catch (e) {
                console.error(e);
            }
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
        if(!this.state.loading && this.state.movieData.length === 0)
            return (
                <div>
                    No Movies in database yet...
                </div>
            )
        const movies = this.state.results;
        
        const result = movies.map((movie, index) => (
            <tr key={movie.title + movie.imdbId}>
                <td>{movie.title}</td>
                <td>{movie.year}</td>
                <td>{movie.imdbId}</td>
                <td></td>
                <td><Link to={`/movieDetails/${movie.imdbId}`}><button type="button" className="btn btn-primary">Details</button></Link></td>
            </tr>
        ));

        return(
            <div className="container">
                <div className="row">
                    <div className="col"></div>
                    <div className="col-8">
                        <div className="row">
                            <label for="search" className="">Search: </label>
                            <input type="text" id="search" className="form-control" onChange={ (event) => this.handleSearch(event)}></input>
                            <button type="button" className="btn btn-primary" onClick={ () => this.search()}>Search</button>
                        </div>
                        <table className="table">
                            <thead>
                                <tr>
                                    <td>Title</td>
                                    <td>Year</td>
                                    <td>Imdb ID</td>
                                    <td>
                                        <button type="button" className="btn btn-primary" onClick={this.sortByAscending}>ASC</button>
                                    </td>
                                    <td>
                                        <button type="button" className="btn btn-primary" onClick={this.sortByDescending}>DESC</button>
                                    </td>
                                </tr>
                            </thead>
                            <tbody>
                            {result}
                            </tbody>
                        </table>
                    </div>
                    <div className="col"></div>
                </div>
            </div>
        )
    }
}

export default Movies;