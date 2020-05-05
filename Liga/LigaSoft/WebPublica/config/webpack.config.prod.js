const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

module.exports = {
  mode: 'production',
  entry: {
    index: "./src/index.js"
  },
  output: {
    path: path.resolve(__dirname, "../dist"),
    filename: "bundle.[hash:6].js"
  },
  plugins: [new CleanWebpackPlugin(),
            new MiniCssExtractPlugin({filename: 'styles.[hash:6].css'}), 
            new HtmlWebpackPlugin({
                inject: true,
                filename: 'index.[hash:6].html',
                template: path.resolve(__dirname, "../src/index.html")})
            ],
  module: {
    rules: [
      {
        loaders: ["babel-loader"],
        test: /\.js$/,
        exclude: /node_modules/
      },
      {
        test: /\.(svg)$/,
        use: {
          loader: 'svg-url-loader',
        },
      },
      {
        test: /\.(ttf|woff|woff2|jpg|png)$/,
        use: {
          loader: 'url-loader',
          options: {
            limit: 8192 // in bytes
          }
        },
      },
      {
        test: /\.css$/,
        exclude: /node_modules/,
        use: [
          MiniCssExtractPlugin.loader,
          {
            loader: "css-loader",
            options: {
              modules: {
                localIdentName: "[hash:base64:5]",
              },
              sourceMap: true                  
            }
          }
        ]
      },
    ]
  },
  resolve: {
    alias: {
      Components: path.resolve(__dirname, '../src/components/'),
      GlobalStyle: path.resolve(__dirname, '../src/assets/styles'),
      Store: path.resolve(__dirname, '../src/store'),
      Utils: path.resolve(__dirname, '../src/utils'),
    }
  }
}