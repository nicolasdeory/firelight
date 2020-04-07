# How to contribute

A lot of help is needed to make this as feature complete as possible!
Check out the roadmap in the [README](README.md) file to see what are the next features that should be implemented
and where the most help is needed. If you have any other suggestions, feel free to implement them!
However, if it is a major change or feature, please make an issue first so we can discuss it.

If you don't know how to code, that's okay! You can always make champion spell animations via a visual editor. 
Check out the [wiki](https://github.com/nicolasdeory/LeagueOfLegendsLED/wiki) for more details!

## Testing

Right now, if you don't have a NodeMCU running WLED or a similar setup that receives LED data via E1.31 sACN protocol, you can test
the lights just by running the program. A debug UI interface will come up which will let you see a virtual LED strip.

## Submitting changes

Please send a [GitHub Pull Request to leagueoflegends-led](https://github.com/nicolasdeory/leagueoflegends-led/pull/new/master) 
and clearly explain what you've done (read more about [pull requests](http://help.github.com/pull-requests/)).

**Please make a pull request to the `dev` branch, not master**.

Make sure all of your commits are atomic (one feature per commit).

Always write a clear log message for your commits. One-line messages are fine for small changes, but bigger changes should look like this:

    $ git commit -m "A brief summary of the commit
    > 
    > A paragraph describing what changed and its impact."

## Code Style
Use this [style guide](https://github.com/raywenderlich/c-sharp-style-guide) when naming variables, namespaces and class members.

Thanks!
