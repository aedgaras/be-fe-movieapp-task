import logo from './logo.svg';
import './App.css';
import { Switch, Route, Link } from 'react-router-dom';
import MovieDetails from './Pages/MovieDetails';
import Movies from './Pages/Movies';

function App() {
  return (
    <div>
      <head>
      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
      </head>
      <Switch>
      <Route exact path="/">
          <Movies />
        </Route>
        <Route path="/movieDetails/:id">
          <MovieDetails />
        </Route>
      </Switch>
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    </div>
  );
}

export default App;
