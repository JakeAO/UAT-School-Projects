from input_data import InputData
from output_data import OutputData


class ResultWriter:
    def write(self, input_data: InputData, output_data: OutputData) -> str:
        result_path = input_data.get_file_path() + '.txt'
        debug_path = input_data.get_file_path() + '.debug.txt'

        # write debug character list to a file
        with open(debug_path, 'w') as file:
            for (debug_index, pred_as_char, pred_value) in output_data.get_debug_characters():
                file.writelines(f"Char: {debug_index} = '{pred_as_char}' ({round(pred_value*100.0)}%)\n")

        # write character list to a file
        with open(result_path, 'w') as file:
            for character in output_data.get_characters():
                file.write(character)

        return result_path
